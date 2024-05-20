using WebApplication1.Entities;
using WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WatervolumeController : ControllerBase
    {
        private readonly DBContext DBContext;
        public WatervolumeController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }
        /// <summary>
        /// Definition du Web Service
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du client a retourné</param>
        /// <response code="200">client selectionné</response>
        /// <response code="404">client introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        /// 


        //Méthode pour récupérer une liste de Réserve d'eau 
        [HttpGet("GetWatervolumes")]
        public async Task<ActionResult<List<Watervolume>>> Get()
        {
            var List = await DBContext.Watervolumes.Select(
            s => new Watervolume
            {
                WatId = s.WatId,
                WatMaxVolume = s.WatMaxVolume,
                WatCurrentVolume = s.WatCurrentVolume,
                WatUnit = s.WatUnit,
                WatInsee = s.WatInsee,
                WatName = s.WatName,
                //Surfacecultures = s.Surfacecultures
            }
            ).ToListAsync();
            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        //Méthode pour récupérer une réserve d'eau grâce à son id
        [HttpGet("GetWatervolumeById")]
        public async Task<ActionResult<Watervolume>> GetWatervolumeById(int Id)
        {
            Watervolume Watervolume = await DBContext.Watervolumes.Select(
            s => new Watervolume
            {
                WatId = s.WatId,
                WatMaxVolume = s.WatMaxVolume,
                WatCurrentVolume = s.WatCurrentVolume,
                WatUnit = s.WatUnit,
                WatInsee = s.WatInsee,
                WatName = s.WatName,
                //Surfacecultures = s.Surfacecultures
            })
            .FirstOrDefaultAsync(s => s.WatId == Id);
            if (Watervolume == null)
            {
                return NotFound();
            }
            else
            {
                return Watervolume;
            }
        }

        //Méthode pour insérer une réserve d'eau
        [HttpPost("InsertWatervolume")]
        public async Task<HttpStatusCode> InsertWatervolume(Watervolume s)
        {
            var entity = new Watervolume()
            {
                WatId = s.WatId,
                WatMaxVolume = s.WatMaxVolume,
                WatCurrentVolume = s.WatCurrentVolume,
                WatUnit = s.WatUnit,
                WatInsee = s.WatInsee,
                WatName = s.WatName
                //Surfacecultures = s.Surfacecultures
            };
            DBContext.Watervolumes.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        //Méthode pour mettre à jour une réserve d'eau
        [HttpPut("UpdateWatervolume")]
        public async Task<HttpStatusCode> UpdateWatervolume(Watervolume Watervolume)
        {
            var entity = await DBContext.Watervolumes.FirstOrDefaultAsync(s => s.WatId == Watervolume.WatId);
            entity.WatMaxVolume = Watervolume.WatMaxVolume;
            entity.WatCurrentVolume = Watervolume.WatCurrentVolume;
            entity.WatId = Watervolume.WatId;
            entity.WatUnit = Watervolume.WatUnit;
            entity.WatInsee = Watervolume.WatInsee;
            entity.WatName = Watervolume.WatName;
            //entity.Surfacecultures = Watervolume.Surfacecultures;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        [HttpDelete("DeleteWatervolume/{Id}")]
        public async Task<HttpStatusCode> DeleteWatervolume(int WatervolumeId)
        {
            var entity = new Watervolume()
            {
                WatId = WatervolumeId
            };
            DBContext.Watervolumes.Attach(entity);
            DBContext.Watervolumes.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        //
        [HttpGet("GetWatervolumeByInsee")]
        public async Task<ActionResult<Watervolume>> GetWatervolumeByInsee(int Insee)
        {
            Watervolume Watervolume = await DBContext.Watervolumes.Select(
            s => new Watervolume
            {
                WatId = s.WatId,
                WatMaxVolume = s.WatMaxVolume,
                WatCurrentVolume = s.WatCurrentVolume,
                WatUnit = s.WatUnit,
                WatInsee = s.WatInsee,
                WatName = s.WatName,
                //Surfacecultures = s.Surfacecultures
            })
            .FirstOrDefaultAsync(s => s.WatInsee == Insee);
            if (Watervolume == null)
            {
                return NotFound();
            }
            else
            {
                return Watervolume;
            }
        }

        [HttpGet("GetAutonomie")]
        public String GetAutonomie(int idKc, int idWat, int idSur)
        {
            Watervolume Reserve = DBContext.Watervolumes.Select(
            s => new Watervolume
            {
                WatId = s.WatId,
                WatMaxVolume = s.WatMaxVolume,
                WatCurrentVolume = s.WatCurrentVolume,
                WatUnit = s.WatUnit,
                WatInsee = s.WatInsee,
                WatName = s.WatName,
            })
            .FirstOrDefault(s => s.WatId == idWat);
            if (Reserve == null)
            {
                return "Réserve inconnue.";
            }
            else
            {
                string apiUriPart1 = "https://api.meteo-concept.com/api/forecast/daily";
                string apiUriPart2 = $"?token=ec041527f2d9577181a87caf3b40edf37cca156ff7c253b1290a65cc809d3091&insee={Reserve.WatInsee}";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync(apiUriPart1 + apiUriPart2).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = response.Content.ReadAsStringAsync().Result;

                        WeatherResponse weatherData = JsonSerializer.Deserialize<WeatherResponse>(responseBody);

                        Kc unKc = DBContext.Kcs.Select(
                        s => new Kc
                        {
                            KcId = s.KcId,
                            KcName = s.KcName,
                            KcNeed = s.KcNeed
                        })
                        .FirstOrDefault(s => s.KcId == idKc);
                        if (unKc == null)
                        {
                            return "Kc inconnu.";
                        }
                        else
                        {
                            Surfaceculture uneSurfaceculture = DBContext.Surfacecultures.Select(
                                s => new Surfaceculture
                                {
                                    SurId = s.SurId,
                                    SurValue = s.SurValue,
                                    SurUnit = s.SurUnit,
                                    WatId = s.WatId
                                })
                                .FirstOrDefault(s => s.SurId == idSur);
                            if (uneSurfaceculture == null)
                            {
                                return "Surface inconnue.";
                            }
                            else
                            {
                                decimal consoParJour = uneSurfaceculture.SurValue * unKc.KcNeed;
                                decimal volumeActuel = Reserve.WatCurrentVolume;
                                int jour = 0;
                                DateTime dateDuJour = DateTime.Today;

                                while (volumeActuel > consoParJour)
                                {
                                    volumeActuel -= consoParJour;
                                    jour++;
                                }
                                DateTime jourRemplirReserveSiSec = dateDuJour.AddDays(jour);
                                

                                List<Forecast> ForecastList = weatherData.forecast;
                                int nbJourDePluie = 0;
                                if(jour < 14)
                                {
                                    for (int i = 0; i < jour + 1; i++)
                                    {
                                        if (ForecastList[i].weather >= 10 && ForecastList[i].weather <= 15)
                                        {
                                            nbJourDePluie++;
                                        }else if (ForecastList[i].weather >= 40 && ForecastList[i].weather <= 48)
                                        {
                                            nbJourDePluie++;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < 14; i++)
                                    {
                                        if (ForecastList[i].weather >= 10 && ForecastList[i].weather <= 15)
                                        {
                                            nbJourDePluie++;
                                        }else if (ForecastList[i].weather >= 40 && ForecastList[i].weather <= 48)
                                        {
                                            nbJourDePluie++;
                                        }
                                    }
                                }

                                DateTime jourRemplirReserve = jourRemplirReserveSiSec.AddDays(nbJourDePluie);


                                return $"Ce champs de {unKc.KcName} consomme {consoParJour} L d'eau par jour. Sans pluie, la réserve se viderait en {jour} jour(s), d'ici là, il pleut {nbJourDePluie} jour(s) donc il faudra remplir la réserve le {jourRemplirReserve.ToString("yyyy-MM-dd")}.";
                            }
                        }
                    }
                    else
                    {
                        return "Aucune information météo pour ce code Insee.";
                    }
                }
            }
        }
    }
}
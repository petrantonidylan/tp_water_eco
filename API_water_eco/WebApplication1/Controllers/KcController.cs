using WebApplication1.Entities;
using WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KcController : Controller
    {
        private readonly DBContext DBContext;
        public KcController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }
        /// <summary>
        /// Definition du Web Service
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du Kc a retourné</param>
        /// <response code="200">Kc selectionné</response>
        /// <response code="404">Kc introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet("GetKcs")]
        public async Task<ActionResult<List<Kc>>> Get()
        {
            var List = await DBContext.Kcs.Select(
            s => new Kc
            {
                KcId = s.KcId,
                KcName = s.KcName,
                KcNeed = s.KcNeed
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
        [HttpGet("GetKcById")]
        public async Task<ActionResult<Kc>> GetKcById(int KcId)
        {
            Kc Kc = await DBContext.Kcs.Select(
            s => new Kc
            {
                KcId = s.KcId,
                KcName = s.KcName,
                KcNeed = s.KcNeed
            })
            .FirstOrDefaultAsync(s => s.KcId == KcId);
            if (Kc == null)
            {
                return NotFound();
            }
            else
            {
                return Kc;
            }
        }
        [HttpPost("InsertKc")]
        public async Task<HttpStatusCode> InsertKc(Kc Kc)
        {
            var entity = new Kc()
            {
                KcId = Kc.KcId,
                KcName = Kc.KcName,
                KcNeed = Kc.KcNeed
            };
            DBContext.Kcs.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }
        [HttpPut("UpdateKc")]
        public async Task<HttpStatusCode> UpdateKc(Kc Kc)
        {
            var entity = await DBContext.Kcs.FirstOrDefaultAsync(s => s.KcId == Kc.KcId);
            entity.KcName = Kc.KcName;
            entity.KcNeed = Kc.KcNeed;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        [HttpDelete("DeleteKc/{Id}")]
        public async Task<HttpStatusCode> DeleteKc(int KcId)
        {
            var entity = new Kc()
            {
                KcId = KcId
            };
            DBContext.Kcs.Attach(entity);
            DBContext.Kcs.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        [HttpGet("GetConsoByKcAndSurface")]
        public async Task<ActionResult<Kc>> GetConsoByKcAndSurface(int KcId, int SurId)
        {
            Kc Kc = await DBContext.Kcs.Select(
            s => new Kc
            {
                KcId = s.KcId,
                KcName = s.KcName,
                KcNeed = s.KcNeed
            })
            .FirstOrDefaultAsync(s => s.KcId == KcId);

            Surfaceculture Surfaceculture = await DBContext.Surfacecultures.Select(
            s => new Surfaceculture
            {
                SurId = s.SurId,
                SurValue = s.SurValue,
                SurUnit = s.SurUnit,
                WatId = s.WatId
            })
            .FirstOrDefaultAsync(s => s.SurId == SurId);

            if( Surfaceculture != null && Kc != null)
            {
                decimal leResultat =  Kc.KcNeed * Surfaceculture.SurValue;
                Kc NewKc = new Kc
                {
                    KcId = 0,
                    KcName = $"La surface fait {Surfaceculture.SurValue} m^2, et le Kc est de {Kc.KcNeed} L/m^2 donc la consommation est de {leResultat} L par jour pour ce champs de {Kc.KcName}.",
                    KcNeed = leResultat
                };
                return NewKc;
            }
            else
            {
                return NotFound();
            }
        }

    }
}
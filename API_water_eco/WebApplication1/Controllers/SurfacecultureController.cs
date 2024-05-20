using WebApplication1.Entities;
using WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurfacecultureController : ControllerBase
    {
        private readonly DBContext DBContext;
        public SurfacecultureController(DBContext DBContext)
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
        [HttpGet("GetSurfacecultures")]
        public async Task<ActionResult<List<Surfaceculture>>> Get()
        {
            var List = await DBContext.Surfacecultures.Select(
            s => new Surfaceculture
            {
                SurId = s.SurId,
                SurValue = s.SurValue,
                SurUnit = s.SurUnit,
                WatId = s.WatId
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
        [HttpGet("GetSurfacecultureById")]
        public async Task<ActionResult<Surfaceculture>> GetSurfacecultureById(int Id)
        {
            Surfaceculture Surfaceculture = await DBContext.Surfacecultures.Select(
            s => new Surfaceculture
            {
                SurId = s.SurId,
                SurValue = s.SurValue,
                SurUnit = s.SurUnit,
                WatId = s.WatId
            })
            .FirstOrDefaultAsync(s => s.SurId == Id);
            if (Surfaceculture == null)
            {
                return NotFound();
            }
            else
            {
                return Surfaceculture;
            }
        }
        [HttpPost("InsertSurfaceculture")]
        public async Task<HttpStatusCode> InsertSurfaceculture(Surfaceculture s)
        {
            var entity = new Surfaceculture()
            {
                SurId = s.SurId,
                SurValue = s.SurValue,
                SurUnit = s.SurUnit,
                WatId = s.WatId
            };
            DBContext.Surfacecultures.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }
        [HttpPut("UpdateSurfaceculture")]
        public async Task<HttpStatusCode> UpdateSurfaceculture(Surfaceculture Surfaceculture)
        {
            var entity = await DBContext.Surfacecultures.FirstOrDefaultAsync(s => s.SurId == Surfaceculture.SurId);
            entity.SurValue = Surfaceculture.SurValue;
            entity.SurUnit = Surfaceculture.SurUnit;
            entity.WatId = Surfaceculture.WatId;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        [HttpDelete("DeleteSurfaceculture/{Id}")]
        public async Task<HttpStatusCode> DeleteSurfaceculture(int SurfacecultureId)
        {
            var entity = new Surfaceculture()
            {
                SurId = SurfacecultureId
            };
            DBContext.Surfacecultures.Attach(entity);
            DBContext.Surfacecultures.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
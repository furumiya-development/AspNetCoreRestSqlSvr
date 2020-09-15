using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCoreRestSqlSvr.Models;

namespace AspNetCoreRestSqlSvr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShohinEntitiesController : ControllerBase
    {
        private readonly ShohinContext _context;

        public ShohinEntitiesController(ShohinContext context)
        {
            _context = context;
        }

        // GET: api/ShohinEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShohinEntity>>> GetShohinItems()
        {
            return await _context.ShohinItems.ToListAsync();
        }

        // GET: api/ShohinEntities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShohinEntity>> GetShohinEntity(int id)
        {
            var shohinEntity = await _context.ShohinItems.FindAsync(id);

            if (shohinEntity == null)
            {
                return NotFound();
            }

            return shohinEntity;
        }

        // PUT: api/ShohinEntities/5
        // オーバーポスト攻撃から保護するには、バインドする特定のプロパティを有効にしてください。
        // 詳細については、https://aka.ms/RazorPagesCRUD を参照してください。
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShohinEntity(int id, ShohinEntity shohinEntity)
        {
            shohinEntity.NumId = id; //Entityの主キーIDへidを入れるべきではないかもしれないが何故か空になっている。
            if (id != shohinEntity.NumId)
            {
                return BadRequest();
            }

            shohinEntity.EditDate = decimal.Parse(String.Format(DateTime.Now.ToString("yyyyMMdd")));
            shohinEntity.EditTime = decimal.Parse(String.Format(DateTime.Now.ToString("HHmmss")));
            _context.Entry(shohinEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShohinEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ShohinEntities
        // オーバーポスト攻撃から保護するには、バインドする特定のプロパティを有効にしてください。
        // 詳細については、https://aka.ms/RazorPagesCRUD を参照してください。
        [HttpPost]
        public async Task<ActionResult<ShohinEntity>> PostShohinEntity(ShohinEntity shohinEntity)
        {
            shohinEntity.EditDate = decimal.Parse(String.Format(DateTime.Now.ToString("yyyyMMdd")));
            shohinEntity.EditTime = decimal.Parse(String.Format(DateTime.Now.ToString("HHmmss")));
            _context.ShohinItems.Add(shohinEntity);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetShohinEntity", new { id = shohinEntity.NumId }, shohinEntity);
            return CreatedAtAction(nameof(GetShohinEntity), new { id = shohinEntity.NumId }, shohinEntity);
        }

        // DELETE: api/ShohinEntities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ShohinEntity>> DeleteShohinEntity(int id)
        {
            var shohinEntity = await _context.ShohinItems.FindAsync(id);
            if (shohinEntity == null)
            {
                return NotFound();
            }

            _context.ShohinItems.Remove(shohinEntity);
            await _context.SaveChangesAsync();

            return shohinEntity;
        }

        private bool ShohinEntityExists(int id)
        {
            return _context.ShohinItems.Any(e => e.NumId == id);
        }
    }
}

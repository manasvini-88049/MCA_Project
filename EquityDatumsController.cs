using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sec_MasterTwo3681.Models;

namespace Sec_MasterTwo3681.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquityDatumsController : ControllerBase
    {
        private readonly SecMasterTwo3681Context _context;

        public EquityDatumsController(SecMasterTwo3681Context context)
        {
            _context = context;
        }

        // GET: api/EquityDatums
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<EquityDatum>>> GetEquityData()
        {
            return await _context.EquityData.ToListAsync();

        }
            //var data = _context.EquityData.ToList();

            //if (data == null)
            //{
            //    return NotFound();

            //}

            //return Ok( data);

        

        // GET: api/EquityDatums/5
        //[HttpGet]
        
        //public async Task<ActionResult<EquityDatum>> GetEquityDatum(int id)
        //{
        //    var equityDatum = await _context.EquityData.FindAsync(id);

        //    if (equityDatum == null)
        //    {
        //        return NotFound();
        //    }

        //    return equityDatum;
        //}

        [HttpGet]
        [Route("Get/{name}")]
        public IActionResult GetEquityByName(string name)
        {
            {
                var data = _context.EquityData.FirstOrDefault(x => x.SecurityName == name);
                if (data == null)
                {
                    return NotFound("The entity by this name does not exist ");
                }
                return Ok(data);
            }

        }



        // PUT: api/EquityDatums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquityDatum(int id, EquityDatum equityDatum)
        {
            if (id != equityDatum.SecurityId)
            {
                return BadRequest();
            }

            _context.Entry(equityDatum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquityDatumExists(id))
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

        // POST: api/EquityDatums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
  
        public async Task<ActionResult<EquityDatum>> PostEquityDatum([FromBody]EquityDatum equityDatum)
        {
            _context.EquityData.Add(equityDatum);
            await _context.SaveChangesAsync();
            return Ok(equityDatum);

            //return CreatedAtAction("GetEquityDatum", new { id = equityDatum.SecurityId }, equityDatum);
        }

        // DELETE: api/EquityDatums/5
        [HttpDelete("{id}")]
        
        public async Task<IActionResult> DeleteEquityDatum(int id)
        {
            var equityDatum = await _context.EquityData.FindAsync(id);
            if (equityDatum == null)
            {
                return NotFound();
            }

            _context.EquityData.Remove(equityDatum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EquityDatumExists(int id)
        {
            return _context.EquityData.Any(e => e.SecurityId == id);
        }
    }
}

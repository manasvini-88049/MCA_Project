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
    public class BondDatumsController : ControllerBase
    {
        private readonly SecMasterTwo3681Context _context;

        public BondDatumsController(SecMasterTwo3681Context context)
        {
            _context = context;
        }

        // GET: api/BondDatums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BondDatum>>> GetBondData()
        {
            return await _context.BondData.ToListAsync();
        }

        // GET: api/BondDatums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BondDatum>> GetBondDatum(int id)
        {
            var bondDatum = await _context.BondData.FindAsync(id);

            if (bondDatum == null)
            {
                return NotFound();
            }

            return bondDatum;
        }


        //[HttpGet]
        //[Route("Get/{name}")]
        //public IActionResult GetBondDataByName(string name)
        //{
        //    if (string.IsNullOrEmpty(name))
        //    {
        //        return BadRequest("name parameter cannot be null or empty.");
        //    }
        //    name = name.Trim();
        //    // Case-insensitive search using EF.Functions.Like
        //    var data = _context.BondData.FirstOrDefault(x => x.SecurityName == "MENTOR 12 1/2 02/15/18");
        ////        .Where(s => EF.Functions.Like(s.SecurityName, '%' +name +"%" ))
        ////.ToList(); // Returns the first matching record, or null if not found

        //    if (data == null)
        //    {
        //        return NotFound("No data found for the given name.");
        //    }

        //    return Ok(data); // Return the data as a response if found
        //}

        [HttpGet]
        [Route("Get/{name}")]
        public IActionResult GetBondDataByISIN(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("isin parameter cannot be null or empty.");
            }

            // Case-insensitive search using EF.Functions.Like
            //var data = _context.BondData.FirstOrDefault(x=>x.Isin==isin);
            var data = _context.BondData.FirstOrDefault(x => x.Isin == name);
            //        .Where(s => EF.Functions.Like(s.SecurityName, '%' +name +"%" ))
            //.ToList(); // Returns the first matching record, or null if not found

            if (data == null)
            {
                return NotFound("No data found for the given name.");
            }

            return Ok(data); // Return the data as a response if found
        }









        // PUT: api/BondDatums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBondDatum(int id, BondDatum bondDatum)
        {
            if (id != bondDatum.SecurityId)
            {
                return BadRequest();
            }

            _context.Entry(bondDatum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BondDatumExists(id))
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

        // POST: api/BondDatums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BondDatum>> PostBondDatum(BondDatum bondDatum)
        {
            _context.BondData.Add(bondDatum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBondDatum", new { id = bondDatum.SecurityId }, bondDatum);
        }

        // DELETE: api/BondDatums/5
        [HttpDelete("{id}")]
     
        public async Task<IActionResult> DeleteBondDatum(int id)
        {
            var bondDatum = await _context.BondData.FindAsync(id);
            if (bondDatum == null)
            {
                return NotFound();
            }

            _context.BondData.Remove(bondDatum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BondDatumExists(int id)
        {
            return _context.BondData.Any(e => e.SecurityId == id);
        }
    }
}

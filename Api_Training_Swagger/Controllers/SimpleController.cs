using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Training_Swagger.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]//, Deprecated = true)]
    [ApiVersion("1.1")]//, Deprecated = true)]
    [ApiVersion("2.0")]

    public class SimpleController : Controller
    {
        private readonly DetailsDb _db;
        public SimpleController(DetailsDb details) 
        {
            this._db = details;
        }
         
        [HttpGet("getData")]
       // [MapToApiVersion("2.0")]
        public string getData()
        {
           return "Connection astablished";
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Data(int id)
        {
            var details = await this._db.Detailss.FindAsync(id);
            if (details != null)
            {
                return Ok(details);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("WriteDetails")]
        public async Task<IActionResult> AddDetailsDbItem([FromBody] Details details)
        {
            this._db.Detailss.Add(details);
            await this._db.SaveChangesAsync();
            return CreatedAtAction(nameof(Data), new { id = details.Id }, details);
        }

        [HttpPut("updateDetails/{id}")]
        public async Task<IActionResult> updateDetails(int id, [FromBody] Details updatedDetails)
        {
            var details = await this._db.Detailss.FindAsync(id);
            if (details == null)
            {
                return BadRequest();
            }
            else
            {
                this._db.Entry(details).State = EntityState.Modified;
                details.Name = updatedDetails.Name;
                details.Family = updatedDetails.Family;
                await this._db.SaveChangesAsync();
                return Ok(details);
            }
        }
        [HttpDelete("deleteDetails/{id}")]
        public async Task<IActionResult> deleteDetails(int id, [FromBody] Details updatedDetails)
        {
            var details = await this._db.Detailss.FindAsync(id);
            if (details == null)
            {
                return BadRequest();
            }
            else
            {
                this._db.Entry(details).State = EntityState.Modified;
                this._db.Detailss.Remove(details);
                await this._db.SaveChangesAsync();
                return Ok(details);
            }
        }
    }
}

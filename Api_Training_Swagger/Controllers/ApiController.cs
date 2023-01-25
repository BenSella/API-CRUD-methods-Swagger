using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Api_Training_Swagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        List<Details> detailList = new List<Details>();
        public ApiController(DetailsDb details)
        {
            detailList.Add(new Details { Id = 1, Name = "Ben", Family = "Sella" });
            detailList.Add(new Details { Id = 2, Name = "Alex", Family = "SEO" });
        }

        [HttpGet("getAllUser")]
        public IActionResult GetUsers()
        {
            return Ok(detailList);
        }

        [HttpGet("getUser")]
        public IActionResult GetUsers(int id)
        {
            Details detail = detailList.Find(u => u.Id == id);
            return Ok(detail);
        }

        [HttpPost("writeDetails")]
        public IActionResult WriteDetails([FromBody] Details details)
        {
            this.detailList.Add(details);
            return Ok(detailList);
        }
        
        [HttpPut("editDetails")]
        public IActionResult EditDetails(int id, [FromBody] Details updatedDetails)
        {
            Details detail = detailList.Find(u => u.Id == id);
            if (detail == null)
            {
                return BadRequest();
            }
            else
            {
                detail.Name = updatedDetails.Name;
                detail.Family = updatedDetails.Family;
                return Ok(detailList);
            }
        }
        [HttpDelete("deleteDetails")]
        public async Task<IActionResult> DeleteDetails(int id)
        {
            Details detail = detailList.Find(u => u.Id == id);
            if (detail == null)
            {
                return BadRequest();
            }
            else
            {
                detailList.Remove(detail);
                return Ok(detailList);
            }
        }
    }
}

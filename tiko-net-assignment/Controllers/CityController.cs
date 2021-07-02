using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using tiko_net_assignment.Models;
using tiko_net_assignment.Services;

namespace tiko_net_assignment.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IDapper _dapper;

        public CityController(IDapper dapper)
        {
            _dapper = dapper;
        }

        // GET: City
        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var result = await Task.FromResult(_dapper.List<City>("Select * FROM Cities"));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: City
        [HttpPost]
        public async Task<IActionResult> PostCity([Bind("Name")] City city)
        {
            if (ModelState.IsValid)
            {
                var dbParams= new DynamicParameters();
                dbParams.Add("@Name", city.Name);
                var result = await Task.FromResult(_dapper.IO<int>("INSERT INTO Cities (Name) VALUES(@Name)", null));

                return Ok(result);
            }
            return BadRequest();
        }
    }
}

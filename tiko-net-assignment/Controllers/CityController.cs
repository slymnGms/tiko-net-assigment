using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tiko_net_assignment.Models;
using tiko_net_assignment.Services;

namespace tiko_net_assignment.Controllers
{
    public class CityController : Controller
    {
        private readonly Dapperr _dapper;

        public CityController(Dapperr dapper)
        {
            _dapper = dapper;
        }

        // GET: City
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

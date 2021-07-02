using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using tiko_net_assignment.Models;
using tiko_net_assignment.Services;

namespace tiko_net_assignment.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly IDapper _dapper;

        public HouseController(IDapper dapper)
        {
            _dapper = dapper;
        }

        // GET: House
        [HttpGet]
        public async Task<IActionResult> GetHouses()
        {
            var result = await Task.FromResult(_dapper.List<House>("Select * FROM Houses WHERE IsDeleted = 0"));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: House/ByAgent/5
        [Route("ByAgent/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetHousesByAgent(int id)
        {
            var dbParams = new DynamicParameters();
            dbParams.Add("@AgentId", id);
            var result = await Task.FromResult(_dapper.ListWithParameters<House>("Select * FROM Houses WHERE AgentId = @AgentId AND IsDeleted = 0", dbParams));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: House/ByCity/5
        [Route("ByCity/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetHousesByCity(int id)
        {
            var dbParams = new DynamicParameters();
            dbParams.Add("@CityId", id);
            var result = await Task.FromResult(_dapper.ListWithParameters<House>("Select * FROM Houses WHERE CityId = @CityId AND IsDeleted = 0", dbParams));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: House
        [HttpPost]
        public async Task<IActionResult> PostHouse([Bind("Price, AgentId, CityId, Address, Description, BedroomCount")] House house)
        {
            if (ModelState.IsValid)
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@Price", house.Price);
                dbParams.Add("@AgentId", house.AgentId);
                dbParams.Add("@CityId", house.CityId);
                dbParams.Add("@Address", house.Address);
                dbParams.Add("@Description", house.Description);
                dbParams.Add("@BedroomCount", house.BedroomCount);
                var result = await Task.FromResult(_dapper.IO<int>("INSERT INTO Houses (Price, AgentId, CityId, Address, Description, BedroomCount) VALUES(@Price, @AgentId, @CityId, @Address, @Description, @BedroomCount)", null));

                return Ok(result);
            }
            return BadRequest();
        }

        // PUT: House/5
        [HttpPut]
        public async Task<IActionResult> PutHouse(int id, [Bind("Id, Price, AgentId, CityId, Address, Description, BedroomCount")] House house)
        {
            if (id != house.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@Price", house.Price);
                dbParams.Add("@Id", house.Id);
                dbParams.Add("@AgentId", house.AgentId);
                dbParams.Add("@CityId", house.CityId);
                dbParams.Add("@Address", house.Address);
                dbParams.Add("@Description", house.Description);
                dbParams.Add("@BedroomCount", house.BedroomCount);
                var result = await Task.FromResult(_dapper.IO<int>("UPDATE Houses SET Price = @Price, CityId = @CityId, AgentId = @AgentId, Address = @Address, Description = @Description, BedroomCount=@BedroomCount WHERE Id = @Id", null));

                return Ok(result);
            }
            return BadRequest();
        }

        // DELETE: House/5
        [HttpDelete]
        public async Task<IActionResult> DeleteHouse(int id)
        {
            if (ModelState.IsValid)
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@Id", id);
                var result = await Task.FromResult(_dapper.IO<int>("UPDATE Houses SET IsDeleted = 1 WHERE Id = @Id", null));

                return Ok(result);
            }
            return BadRequest();
        }

    }
}

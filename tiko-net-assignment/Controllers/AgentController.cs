using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using tiko_net_assignment.Models;
using tiko_net_assignment.Services;

namespace tiko_net_assignment.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IDapper _dapper;

        public AgentController(IDapper dapper)
        {
            _dapper = dapper;
        }

        // GET: Agent
        [HttpGet]
        public async Task<IActionResult> GetAgents()
        {
            var result = await Task.FromResult(_dapper.List<Agent>("Select * FROM Agents WHERE IsDeleted = 0"));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: Agent
        [HttpPost]
        public async Task<IActionResult> PostAgent([Bind("Name, CityId")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@Name", agent.Name);
                dbParams.Add("@CityId", agent.CityId);
                var result = await Task.FromResult(_dapper.IO<int>("INSERT INTO Agents (Name, CityId, IsDeleted) VALUES(@Name, @CityId, 0)", null));

                return Ok(result);
            }
            return BadRequest();
        }

         // DELETE: Agent/5
        [HttpDelete]
        public async Task<IActionResult> DeleteAgent(int id)
        {
            if (ModelState.IsValid)
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@Id", id);
                var result = await Task.FromResult(_dapper.IO<int>("UPDATE Agents SET IsDeleted = 1 WHERE Id = @Id", null));

                return Ok(result);
            }
            return BadRequest();
        }
    }
}

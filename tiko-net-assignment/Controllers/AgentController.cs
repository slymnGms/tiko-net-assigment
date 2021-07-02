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
    public class AgentController : Controller
    {
        private readonly Dapperr _dapper;

        public AgentController(Dapperr dapper)
        {
            _dapper = dapper;
        }

        // GET: Agent
        public async Task<IActionResult> GetAgents()
        {
            var result = await Task.FromResult(_dapper.List<Agent>("Select * FROM Agents"));
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
                var result = await Task.FromResult(_dapper.IO<int>("INSERT INTO Agents (Name, CityId) VALUES(@Name, @CityId)", null));

                return Ok(result);
            }
            return BadRequest();
        }

        // PUT: Agent/5
        [HttpPut]
        public async Task<IActionResult> PutAgent(int id, [Bind("Id, Name, CityId")] Agent agent)
        {
            if (id != agent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@Id", agent.Id);
                var result = await Task.FromResult(_dapper.IO<int>("DELETE FROM Agents WHERE Id = @Id", null));

                return Ok(result);
            }
            return BadRequest();
        }
    }
}

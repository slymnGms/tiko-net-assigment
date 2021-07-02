using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tiko_net_assignment.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult GetHome()
        {
            return Ok("Welcome To API");
        }
    }
}

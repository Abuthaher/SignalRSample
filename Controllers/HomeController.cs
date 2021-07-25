using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SampleNotifiaction.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleNotifiaction.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<MyHub> _hubContext;

        public HomeController(ILogger<HomeController> logger, IHubContext<MyHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetData()
        {
            var repository = new Repository.Repository();
            return new JsonResult(repository.LoadData());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Fiap2.Core;
using Microsoft.AspNetCore.Mvc;
using Fiap2.Web.Models;

namespace Fiap2.Web.Controllers
{
    public class HomeController : Controller
    {
        private IPhotoService _photoService;

        public HomeController(IPhotoService photoService)
        {
            _photoService = photoService;
        }
        public IActionResult Index()
        {
            //var service = new Fiap2.Core.PhotoService();
            //var photos = service.List();
        
            var photos = _photoService.List();

            return View(photos);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Fiap2.Core;
using Fiap2.Web.Custom;
using Microsoft.AspNetCore.Mvc;
using Fiap2.Web.Models;

namespace Fiap2.Web.Controllers
{
    public class PhotosController : Controller
    {
        private IPhotoService _photoService;

        public PhotosController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        //[ResponseCache(Duration = 200)]
        [Cache(Duration = 30)]
        public IActionResult Index()
        {
            //var service = new Fiap2.Core.PhotoService();
            //var photos = service.List();

            var photos = _photoService.List();

            return View(photos);
        }

        public IActionResult Category(string category, int? total)
        {
            ViewBag.Category = category;
            var photos = _photoService.List(category).Take(total ?? 20);

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

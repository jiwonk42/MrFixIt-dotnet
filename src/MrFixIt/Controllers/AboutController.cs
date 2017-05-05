using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MrFixIt.Controllers
{
    public class AboutController : Controller
    {
        // Displays 'About' page with no functionality
        public IActionResult Index()
        {
            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrFixIt.Models;
using Microsoft.EntityFrameworkCore;

namespace MrFixIt.Controllers
{
    public class WorkersController : Controller
    {
        private MrFixItContext db = new MrFixItContext();
        
        // Displays a Worker profile with their jobs, displays a link where a Worker can create their Worker profile if they do not have a profile
        public IActionResult Index()
        {
            var thisWorker = db.Workers.Include(i =>i.Jobs).FirstOrDefault(i => i.UserName == User.Identity.Name);
            if (thisWorker != null)
            {
                return View(thisWorker);
            }
            else
            {
                return RedirectToAction("Create");
            }
        }

        // Displays a form where a Worker can submit their profile
        public IActionResult Create()
        {
            return View();
        }

        // Creates a Worker profile with their First Name and Last Name
        [HttpPost]
        public IActionResult Create(string firstName, string lastName)
        {
            Worker newWorker = new Worker(FirstName: firstName, LastName: lastName);
            newWorker.UserName = User.Identity.Name;
            db.Workers.Add(newWorker); 
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

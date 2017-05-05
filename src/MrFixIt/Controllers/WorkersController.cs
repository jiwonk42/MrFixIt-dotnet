using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrFixIt.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IActionResult Create(Worker worker)
        {
            worker.UserName = User.Identity.Name;
            db.Workers.Add(worker);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Displays a Job Details where a User claims it
        public IActionResult Perform(int id)
        {
            var thisJob = db.Jobs.Include(jobs => jobs.Worker).FirstOrDefault(items => items.JobId == id);
            return View(thisJob);
        }

        // Allows a Worker to claim the (specific) job once clicked
        [HttpPost]
        public IActionResult Perform(Job job)
        {
            job.Worker = db.Workers.FirstOrDefault(i => i.UserName == User.Identity.Name);
            job.Pending = true;
            db.Entry(job).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

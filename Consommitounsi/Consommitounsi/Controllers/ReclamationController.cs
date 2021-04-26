using Consommitounsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Consommitounsi.Controllers
{
    public class ReclamationController : Controller
    {
        // GET: Reclamation
        public ActionResult Index(String searchString)
        {
            IEnumerable<Reclamation> events = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080/reclamation/");
                var responseTask = client.GetAsync("showReclamation");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Reclamation>>();
                    readJob.Wait();
                    events = readJob.Result;
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        events = events.Where(m => m.subject.Contains(searchString)).ToList();
                    }
                    return View(events);
                }
                else
                {
                    events = Enumerable.Empty<Reclamation>();
                    ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
                }
            }
            return View(events);
        }
        [HttpPost]
        public ActionResult Create(Reclamation evt)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080/reclamtion/");
                var postJob = client.PostAsJsonAsync<Reclamation>("addReclamation", evt);
                postJob.Wait();

                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
                return View(evt);
            }
        }
        public ActionResult Delete(Reclamation evt)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080/reclamtion/");
                var postJob = client.PostAsJsonAsync<Reclamation>("deleteReclamation", evt);
                postJob.Wait();
                if(postJob.Result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Server error occured. Please contact admin for help!");
                return View(evt);

            }
        }
    }
    
}
using Consommitounsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Reclamation epm)
        {
            string Baseurl = "http://localhost:8080/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // client.DefaultRequestHeaders.Add("X-Miva-API-Authorization", "MIVA xxxxxxxxxxxxxxxxxxxxxx");

                epm.created = DateTime.UtcNow;
                epm.decision = "UNTREATED";
                var response = await client.PostAsJsonAsync("reclamation/addReclamation", epm);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(epm);
        }

        public ActionResult Delete(long id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080/reclamation/");
                var deleteTask = client.GetAsync("deleteReclamation/" + id.ToString());

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
        }
        public ActionResult Details(long id)
        {
            Reclamation products = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080/reclamation/");
                var responseTask = client.GetAsync("fetchById/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Reclamation>();
                    readTask.Wait();

                    products = readTask.Result;
                }
            }
            return View(products);
        }
        [HttpPost]
        public  ActionResult Edit(Reclamation epm)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080/");
                epm.created = DateTime.UtcNow;
                epm.decision = "UNTREATED";
                var putTask = client.PostAsJsonAsync<Reclamation>("reclamation/editReclamation", epm);
                putTask.Wait();

                var ressult = putTask.Result;
                if (ressult.IsSuccessStatusCode)

                    return RedirectToAction("Index");
                return View(epm);

            }


        }
        public ActionResult Edit(long id)
        {
            Reclamation products = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080/reclamation/");
                var responseTask = client.GetAsync("fetchById/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Reclamation>();
                    readTask.Wait();

                    products = readTask.Result;
                }
            }
            return View(products);
        }
    }
    
}
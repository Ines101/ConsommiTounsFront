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
    public class EventController : Controller
    {
        // GET: Event
        // GET: Shipment
        public ActionResult Index(String searchString)
        {
            IEnumerable<Event> events = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080/event/");
                var responseTask = client.GetAsync("retrieve-all-Events");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Event>>();
                    readJob.Wait();
                    events = readJob.Result;
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        events = events.Where(m => m.name_e.Contains(searchString)).ToList();
                    }
                    return View(events);
                }
                else
                {
                    events = Enumerable.Empty<Event>();
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
        public async Task<ActionResult> Create(Event epm)
        {
            string Baseurl = "http://localhost:8080/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // client.DefaultRequestHeaders.Add("X-Miva-API-Authorization", "MIVA xxxxxxxxxxxxxxxxxxxxxx");


                var response = await client.PostAsJsonAsync("event/add-Event/", epm);
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
                client.BaseAddress = new Uri("http://localhost:8080/event/");
                var deleteTask = client.DeleteAsync("remove-Event/" + id.ToString());

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
            Event products = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080/event/");
                var responseTask = client.GetAsync("retrieve-Event/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Event>();
                    readTask.Wait();

                    products = readTask.Result;
                }
            }
            return View(products);
        }
        [HttpPost]
        public ActionResult Edit(Event epm)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080/");
                var putTask = client.PutAsJsonAsync<Event>("event/modify-Event", epm);
                putTask.Wait();

                var ressult = putTask.Result;
                if (ressult.IsSuccessStatusCode)

                    return RedirectToAction("Index");
                return View(epm);

            }
        }


        
        public ActionResult Edit(long id)
        {
            Event products = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080/event/");
                var responseTask = client.GetAsync("retrieve-Event/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Event>();
                    readTask.Wait();

                    products = readTask.Result;
                }
            }
            return View(products);
        }
    }
}
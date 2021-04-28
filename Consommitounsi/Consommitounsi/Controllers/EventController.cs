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
                        events = events.Where(m => m.Name_e.Contains(searchString)).ToList();
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
        public ActionResult Details(int id)
        {
            Event events = null;

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

                    events = readTask.Result;
                }
            }
            return View(events);
        }

        //Delete a event
        public ActionResult Delete(int id)
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
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Event evt)
        {
            string Baseurl = "http://localhost:8080/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // client.DefaultRequestHeaders.Add("X-Miva-API-Authorization", "MIVA xxxxxxxxxxxxxxxxxxxxxx");


                var response = await client.PostAsJsonAsync("event/add-Event", evt);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(evt);
        }






    }
}
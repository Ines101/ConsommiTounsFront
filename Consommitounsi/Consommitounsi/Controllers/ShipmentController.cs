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
    public class ShipmentController : Controller
    {
        // GET: Shipment
        public ActionResult Index(String searchString)
        {
            IEnumerable<Shipment> events = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/pidev/servlet/");
                var responseTask = client.GetAsync("retrieve-all-Shipments");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Shipment>>();
                    readJob.Wait();
                    events = readJob.Result;
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        events = events.Where(m => m.status.Contains(searchString)).ToList();
                    }
                    return View(events);
                }
                else
                {
                    events = Enumerable.Empty<Shipment>();
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
            string Baseurl = "http://localhost:8086/pidev/servlet/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // client.DefaultRequestHeaders.Add("X-Miva-API-Authorization", "MIVA xxxxxxxxxxxxxxxxxxxxxx");


                var response = await client.PostAsJsonAsync("add-Shipment/", epm);
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
                client.BaseAddress = new Uri("http://localhost:8086/pidev/servlet/");
                var deleteTask = client.DeleteAsync("remove-Shipment/" + id.ToString());

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
            Shipment products = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/pidev/servlet/");
                var responseTask = client.GetAsync("retrieve-Event/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Shipment>();
                    readTask.Wait();

                    products = readTask.Result;
                }
            }
            return View(products);
        }
        [HttpPost]
        public ActionResult Edit(Shipment epm)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/pidev/servlet/");
                var putTask = client.PutAsJsonAsync<Shipment>("modify-Shipment", epm);
                putTask.Wait();

                var ressult = putTask.Result;
                if (ressult.IsSuccessStatusCode)

                    return RedirectToAction("Index");
                return View(epm);

            }
        }



        public ActionResult Edit(long id)
        {
            Shipment products = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8086/pidev/servlet/");
                var responseTask = client.GetAsync("retrieve-Shipment/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Shipment>();
                    readTask.Wait();

                    products = readTask.Result;
                }
            }
            return View(products);
        }
    }
}
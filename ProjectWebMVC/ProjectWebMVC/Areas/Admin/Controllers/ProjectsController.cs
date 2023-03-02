using Newtonsoft.Json;
using ProjectWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ProjectWebMVC.Areas.Admin.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: Admin/Projects
        string BASE_URI = "http://localhost:57076/api/Projects/";

        public ActionResult Index(string ProjectName)
        {
            using (HttpClient client = new HttpClient())
            {
                IEnumerable<Project> empList;
                if (ProjectName != null)
                {
                    HttpResponseMessage responseMessage = GlobalVariables.WebApiClient.GetAsync("Projects/Search?ProjectName=" + ProjectName).Result;
                    empList = responseMessage.Content.ReadAsAsync<IEnumerable<Project>>().Result;
                    return View(empList);

                }
                else if (ProjectName == null)
                {
                    HttpResponseMessage responseMessage = GlobalVariables.WebApiClient.GetAsync("GetAllProjects").Result;
                    empList = responseMessage.Content.ReadAsAsync<IEnumerable<Project>>().Result;
                    return View(empList);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }


        // GET: Admin/Projects/Details/5
        public ActionResult Details(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);
                var getTask = client.GetAsync("" + id);
                getTask.Wait();
                var result = getTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    Project p = JsonConvert.DeserializeObject<Project>(data);
                    return View(p);
                }
                return View();
            }
        }

        // GET: Admin/Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Projects/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BASE_URI);
                    Project p = new Project();

                    p.ProjectID = int.Parse(collection["ProjectID"]);
                    p.ProjectName = collection["ProjectName"];
                    p.StartDate = DateTime.Parse( collection["StartDate"]);
                    p.EndDate =DateTime.Parse( collection["EndDate"]);
                    p.TotalMoney =float.Parse( collection["TotalMoney"]);
                    string data = JsonConvert.SerializeObject(p);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    var postTask = client.PostAsync("", content);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Projects/Edit/5
        public ActionResult Edit(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);
                var getTask = client.GetAsync("" + id);
                getTask.Wait();
                var result = getTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    Project p = JsonConvert.DeserializeObject<Project>(data);
                    return View(p);
                }
                return View();
            }
        }

        // POST: Admin/Projects/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BASE_URI);
                    Project p = new Project();

                    p.ProjectID = int.Parse(collection["ProjectID"]);
                    p.ProjectName = collection["ProjectName"];
                    p.StartDate = DateTime.Parse(collection["StartDate"]);
                    p.EndDate = DateTime.Parse(collection["EndDate"]);
                    p.TotalMoney = float.Parse(collection["TotalMoney"]);

                    string data = JsonConvert.SerializeObject(p);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    var putTask = client.PutAsync("" + id, content);
                    putTask.Wait();
                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }
                    return View();
                }


            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Projects/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Admin/Projects/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        public ActionResult Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URI);
                var getTask = client.GetAsync("DeleteProject/" + id);
                getTask.Wait();
            }
            return RedirectToAction("Index");
        }
    }
}

using Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace AspApi.Controllers
{
    public class DeptController : Controller
    {
        // GET: Dept
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44343/api/")
        };
        public JsonResult cloadDepartment()
        {
            IEnumerable<Department> models = null;
            var responsTask = client.GetAsync("dept");
            responsTask.Wait();
            var result = responsTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Department>>();
                readTask.Wait();
                models = readTask.Result;
            }
            else
            {
                models = Enumerable.Empty<Department>();
                ModelState.AddModelError(string.Empty, "server error, try later");
            }
            //return Json(new { data = models }, JsonRequestBehavior.AllowGet);
            return new JsonResult { Data = models, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        // GET: Dept
        public ActionResult Index()
        {
            return View(cloadDepartment());
        }
        public JsonResult Insert(Department department)
        {
            var myContent = JsonConvert.SerializeObject(department);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            if (department.Id == 0)
            {
                var result = client.PostAsync("Dept", byteContent).Result;
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return Json("Internal server error");
        }
    }
}
using Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (department.Id == 0) //input
            {
                var result = client.PostAsync("dept", byteContent).Result;
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else //update
            {
                var result = client.PutAsync("dept/" + department.Id, byteContent).Result;
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public async Task<JsonResult> GetById(int Id)
        {
            HttpResponseMessage response = await client.GetAsync("dept");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<IList<Department>>();
                var dept = data.FirstOrDefault(s => s.Id == Id);
                var json = JsonConvert.SerializeObject(dept, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return new JsonResult { Data = json, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return Json("Internal Server Error");
        }
        public JsonResult Delete(int Id)
        {
            var result = client.DeleteAsync("dept/" + Id).Result;
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        //public JsonResult InsertOrUpdate(Department department)
        //{
        //    var myContent = JsonConvert.SerializeObject(department);
        //    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //    var byteContent = new ByteArrayContent(buffer);
        //    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //    if (department.Id == 0) // insert
        //    {
        //        var result = client.PostAsync("department", byteContent).Result;
        //        return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //    }
        //    else // update
        //    {
        //        var result = client.PutAsync("department/" + department.Id, byteContent).Result;
        //        return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //    }
        //}
    }
}
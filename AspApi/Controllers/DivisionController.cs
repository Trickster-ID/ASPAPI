using Api.Context;
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
    public class DivisionController : Controller
    {
        myContext conn = new myContext();
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44343/api/")
        };
        public JsonResult cloadDivisi()
        {
            IEnumerable<DivisiVM> models = null;
            var responsTask = client.GetAsync("divisi");
            responsTask.Wait();
            var result = responsTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<DivisiVM>>();
                readTask.Wait();
                models = readTask.Result;
            }
            else
            {
                models = Enumerable.Empty<DivisiVM>();
                ModelState.AddModelError(string.Empty, "server error, try later");
            }
            //return Json(new { data = models }, JsonRequestBehavior.AllowGet);
            return new JsonResult { Data = models, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        // GET: Division
        public ActionResult Index()
        {
            return View(cloadDivisi());
        }
        public JsonResult Insert(DivisionModel divisionModel)
        {
            var myContent = JsonConvert.SerializeObject(divisionModel);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (divisionModel.Id == 0) //input
            {
                var result = client.PostAsync("divisi", byteContent).Result;
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else //update
            {
                var result = client.PutAsync("divisi/" + divisionModel.Id, byteContent).Result;
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public async Task<JsonResult> GetById(int Id)
        {
            HttpResponseMessage response = await client.GetAsync("divisi");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<IList<DivisiVM>>();
                var divisi = data.FirstOrDefault(s => s.Id == Id);
                var json = JsonConvert.SerializeObject(divisi, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return new JsonResult { Data = json, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return Json("Internal Server Error");
        }
        public JsonResult Delete(int Id)
        {
            var result = client.DeleteAsync("divisi/" + Id).Result;
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
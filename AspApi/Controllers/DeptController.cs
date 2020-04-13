using Api.Models;
using AspApi.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
        public async Task<ActionResult> Excel()
        {
            var columnHeaders = new string[]
            {
                "Name",
                "Ditambahkan"
            };

            byte[] result;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Department Excel");
                using (var cells = worksheet.Cells[1, 1, 1, 2])
                {
                    cells.Style.Font.Bold = true;
                }

                for (var i = 0; i < columnHeaders.Count(); i++)
                {
                    worksheet.Cells[1, i + 1].Value = columnHeaders[i];
                }

                var j = 2;
                HttpResponseMessage response = await client.GetAsync("dept");
                if (response.IsSuccessStatusCode)
                {
                    var readTask = await response.Content.ReadAsAsync<IList<Department>>();
                    foreach (var department in readTask)
                    {
                        worksheet.Cells["A" + j].Value = department.Name;
                        worksheet.Cells["B" + j].Value = department.CreateDate.ToString("MM/dd/yyyy");
                        j++;
                    }
                }
                result = package.GetAsByteArray();
            }
            return File(result, "application/ms-excel", $"Department-{DateTime.Now.ToString("hh:mm:ss-MM/dd/yyyy")}.xlsx");
        }
        public async Task<ActionResult> CSV()
        {
            var columnHeaders = new string[]
            {
                "Nama Department",
                "Tanggal Ditambahkan"
            };
            HttpResponseMessage response = await client.GetAsync("dept");
            var readTask = await response.Content.ReadAsAsync<IList<Department>>();
            var departmentRecords = from department in readTask
            select new object[]{
                    $"{department.Name}",
                    $"\"{department.CreateDate.ToString("MM/dd/yyyy")}\""
            }.ToList();
            var departmentcsv = new StringBuilder();
            departmentRecords.ForEach(line =>
            {
                departmentcsv.AppendLine(string.Join(",", line));
            });
            byte[] buffer = Encoding.ASCII.GetBytes($"{string.Join(",", columnHeaders)}\r\n{departmentcsv.ToString()}");
            return File(buffer, "text/csv", $"Department-{DateTime.Now.ToString("hh:mm:ss-MM/dd/yyyy")}.csv");
        }
        public ActionResult Report(Department department)
        {
            DeptReport deptreport = new DeptReport();
            byte[] abytes = deptreport.PrepareReport(exportToPdf());
            return File(abytes, "application/pdf");
        }

        public List<Department> exportToPdf()
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
            return models.ToList();
        }
    }
}
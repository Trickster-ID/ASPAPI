using Api.Models;
using Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Api.Controllers
{
    public class DivisiController : ApiController
    {
        DivisionRepository divisi = new DivisionRepository();
        [HttpGet]
        public IHttpActionResult Get()
        {
            if (divisi.Get() == null)
            {
                return Content(HttpStatusCode.NotFound, "Data Divisi is Empty!");
            }
            return Ok(divisi.Get());
        }

        [HttpGet]
        [ResponseType(typeof(DivisiVM))]
        public async Task<IEnumerable<DivisiVM>> Get(int Id)
        {
            if (await divisi.Get(Id) == null)
            {
                return null;
            }
            return await divisi.Get(Id);
        }
        public IHttpActionResult Post(DivisionModel divisis)
        {
            if ((divisis.Nama != null) || (divisis.Nama != ""))
            {
                divisi.Create(divisis);
                return Ok("Division Add Successfully!"); //Status 200 OK
            }
            return BadRequest("Failed to Add Division");
        }
        public IHttpActionResult Put(int Id, DivisionModel divisis)
        {
            if ((divisis.Nama != null) && (divisis.Nama != ""))
            {
                divisi.Update(Id, divisis);
                return Ok("Division Updated Successfully!"); //Status 200 OK
            }
            return BadRequest("Failed to Update Division");
        }
        public IHttpActionResult Delete(int Id)
        {
            var delete = divisi.Delete(Id);
            if (delete > 0)
            {
                return Ok("Division Delete Successfully");
            }
            return BadRequest("Failed to Delete");
        }
    }
}

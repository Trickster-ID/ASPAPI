using Api.Context;
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
    public class DeptController : ApiController
    {
        
        DepartmentRepository departments = new DepartmentRepository();
        [HttpGet]
        public IHttpActionResult Get()
        {
            if (departments.Get() == null)
            {
                return Content(HttpStatusCode.NotFound, "Data Department is Empty!");
            }
            return Ok(departments.Get());
        }
        [HttpGet]
        [ResponseType(typeof(Department))]
        public async Task<IEnumerable<Department>> Get(int Id)
        {
            if (await departments.Get(Id) == null)
            {
                return null;
            }
            return await departments.Get(Id);
        }
        public IHttpActionResult Post(Department department)
        {
            if ((department.Name != null) || (department.Name != ""))
            {
                departments.Create(department);
                return Ok("Department Add Successfully!");
            }
            return BadRequest("Failed to Add Department");
        }
        public IHttpActionResult Put(int Id, Department department)
        {
            if ((department.Name != null) && (department.Name != ""))
            {
                departments.Update(Id, department);
                return Ok("Department Updated Successfully!");
            }
            return BadRequest("Failed to Update Department");
        }
        public IHttpActionResult Delete(int Id)
        {
            var delete = departments.Delete(Id);
            if (delete > 0)
            {
                return Ok("Department Delete Successfully");
            }
            return BadRequest("Failed to Delete");
        }
    }
}

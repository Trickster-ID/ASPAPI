using Api.Context;
using Api.Migrations;
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
        myContext conn123 = new myContext();
        [HttpGet]
        public IEnumerable<Department> Get()
        {
            return departments.Get();
        }
        [HttpGet]
        [ResponseType(typeof(Department))]
        public async Task<IEnumerable<Department>> Get(int Id)
        {
            return await departments.Get(Id);
        }
        public IHttpActionResult Post(Department department)
        {
            if (department.Name == "")
            {
                return Content(HttpStatusCode.NotFound, "Failed to Add Department");
            }
            departments.Create(department);
            return Ok("Department Add Successfully");
        }
        public IHttpActionResult Put(int Id, Department department)
        {
            var put = departments.Update(Id, department);
            if (put > 0)
            {
                return Ok("Department Update Successfully");
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
            //var dept_id = conn.Departments.FirstOrDefault(x => x.Id == Id);

            //if (dept_id == null)
            //{
            //    return BadRequest("Failed to delete department");
            //}
            //else
            //{
            //    departments.Delete(Id);
            //    return Ok("Deleted successfully");
            //}
        }
        //public HttpResponseMessage (int Id)
        //{
        //    var nama = departments
        //}
    }
}

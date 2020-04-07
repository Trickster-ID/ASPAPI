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
            var post = departments.Create(department);
            if (post > 0)
            {
                return Ok("Department Add Successfully");
            }
            return BadRequest("Failed to Add Department");
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
        }
    }
}

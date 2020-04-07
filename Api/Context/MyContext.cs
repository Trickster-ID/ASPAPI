using Api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Api.Context
{
    public class MyContext : DbContext
    {
        public MyContext() : base("conapi") { }
        public DbSet<Department> Departments { get; set; }
    }
}
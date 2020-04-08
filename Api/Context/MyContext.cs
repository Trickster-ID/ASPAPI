using Api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Api.Context
{
    public class myContext : DbContext
    {
        public myContext() : base("conapi") { }
        public DbSet<Department> Departments { get; set; }
    }
}
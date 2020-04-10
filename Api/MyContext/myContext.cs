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
        public myContext() : base("DB_API") { }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DivisionModel> DivisionModels { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class DivisiVM
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public Nullable<DateTimeOffset> UpdateDate { get; set; }
    }
}
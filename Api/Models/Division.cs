using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Api.Models
{
    [Table("TB_M_Division")]
    public class DivisionModel
    {
        [Key]
        public int Id { get; set; }
        public string Nama { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public bool IsDelete { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public Nullable<DateTimeOffset> UpdateDate { get; set; }
        public Nullable<DateTimeOffset> DeleteDate { get; set; }

        public DivisionModel() { }
        public DivisionModel(DivisionModel divisionModel) //create
        {
            this.Nama = divisionModel.Nama;
            this.CreateDate = DateTimeOffset.Now;
            this.IsDelete = false;
            this.DepartmentId = Department.Id;
        }

        public void Update(DivisionModel divisionModel)
        {
            this.Nama = divisionModel.Nama;
            this.UpdateDate = DateTimeOffset.Now;
            this.DepartmentId = Department.Id;
        }

        public void Delete()
        {
            this.IsDelete = true;
            this.DeleteDate = DateTimeOffset.Now;
        }
    }
}
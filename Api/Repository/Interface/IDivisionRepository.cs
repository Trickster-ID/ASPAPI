using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repository.Interface
{
    interface IDivisionRepository
    {
        IEnumerable<DivisiVM> Get();
        Task<IEnumerable<DivisiVM>> Get(int Id);
        int Create(DivisionModel division);
        int Update(int Id, DivisionModel division);
        int Delete(int Id);
    }
}

using Api.Models;
using Api.Repository.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Api.Repository
{
    public class DivisionRepository : IDivisionRepository
    {
        DynamicParameters parameters = new DynamicParameters();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        public int Create(DivisionModel division)
        {
            var procName = "SP_Insert_Divisi";
            parameters.Add("@Nama", division.Nama);
            parameters.Add("@Id", division.DepartmentId);
            var create = conn.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
            return create;
        }

        public int Delete(int Id)
        {
            var procName = "SP_Delete_Divisi";
            parameters.Add("@Id", Id);
            var update = conn.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
            return update;
        }

        public IEnumerable<DivisiVM> Get()
        {
            var procName = "SP_View_Divisi";
            var getalldivisi = conn.Query<DivisiVM>(procName, commandType: CommandType.StoredProcedure);
            return getalldivisi;
        }

        public async Task<IEnumerable<DivisiVM>> Get(int Id)
        {
            var asName = "SP_ViewById_Divisi";
            parameters.Add("@Id", Id);
            var getDivisi = await conn.QueryAsync<DivisiVM>(asName, parameters, commandType: CommandType.StoredProcedure);
            return getDivisi;
        }

        public int Update(int Id, DivisionModel division)
        {
            var procName = "SP_Update_Divisi";
            parameters.Add("@Id", Id);
            parameters.Add("@IdDept", division.DepartmentId);
            parameters.Add("@Name", division.Nama);
            var update = conn.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
            return update;
        }
    }
}
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
using static Dapper.SqlMapper;

namespace Api.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        DynamicParameters parameters = new DynamicParameters();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        public int Create(Department department)
        {
            //throw new NotImplementedException();
            var procName = "SP_InsertDepartment";
            parameters.Add("@Name", department.Name);
            var create = conn.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
            return create;
        }

        public int Delete(int Id)
        {
            //throw new NotImplementedException();
            var procName = "SP_Delete";
            parameters.Add("@id", Id);
            var update = conn.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
            return update;
        }

        public IEnumerable<Department> Get()
        {
            //throw new NotImplementedException();
            var procName = "SP_ViewDept";
            var getalldepartment = conn.Query<Department>(procName, commandType: CommandType.StoredProcedure);
            return getalldepartment;
        }

        public async Task<IEnumerable<Department>> Get(int Id)
        {
            //throw new NotImplementedException();
            var asName = "SP_ViewById";
            parameters.Add("@Id", Id);
            var getDept = await conn.QueryAsync<Department>(asName, parameters, commandType: CommandType.StoredProcedure);
            return getDept;
        }

        public int Update(int Id, Department department)
        {
            //throw new NotImplementedException();
            var procName = "SP_Update";
            parameters.Add("@newid", Id);
            parameters.Add("@Name", department.Name);
            var update = conn.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
            return update;
        }
    }
}
using Dapper;
using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Infrastructure.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        /// <summary>
        /// thực hiện lấy dữ liệu từ db lọc và phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        /// Author HieuNv
        public PagingResult<Employee> GetFilter(int pageSize, int pageNumber, string searchTerms)
        {
            using (var dbConnection = DatabaseConnection.DbConnection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@SearchTerms", searchTerms);
                parameters.Add("@PageSize", pageSize);
                parameters.Add("@PageNumber", pageNumber);
                parameters.Add("TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var procName = $"Proc_GetFilterPagingEmployees";

                var employees = dbConnection.Query<Employee>(procName, param: parameters, commandType: CommandType.StoredProcedure);
                var TotalRecord = parameters.Get<int>("TotalRecord");
                return new PagingResult<Employee>(TotalRecord, (List<Employee>)employees);
            }
        }
    }
}

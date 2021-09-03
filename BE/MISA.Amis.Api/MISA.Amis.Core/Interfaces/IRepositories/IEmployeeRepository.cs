using MISA.Amis.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Interfaces.IRepositories
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// lấy dữ liệu tìm kiếm và phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        /// Author HieuNv
        PagingResult<Employee> GetFilter(int pageSize, int pageNumber,string searchTerms);
    }
}

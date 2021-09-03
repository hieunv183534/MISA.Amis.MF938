using MISA.Amis.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Interfaces.IServices
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Lấy danh sách nhân viên theo tiêu chí lọc và phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        /// Author HieuNv
        ServiceResult GetFilter(int pageSize, int pageNumber, string searchTerms);
    }
}

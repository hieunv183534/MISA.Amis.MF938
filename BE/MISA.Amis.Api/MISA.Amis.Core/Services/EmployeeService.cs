using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Interfaces.IRepositories;
using MISA.Amis.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Declare

        IEmployeeRepository _employeeRepository;

        #endregion

        #region Constructor

        public EmployeeService(IEmployeeRepository employeeRepository, IBaseRepository<Employee> baseRepository) : base(baseRepository)
        {
            _employeeRepository = employeeRepository;
        }

        #endregion

        /// <summary>
        /// Xử lí thêm cho add nhân viên
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Author HieuNv
        public override ServiceResult Add(Employee entity)
        {
            // validate riêng cho employee
            // validate mã nhân viên dạng NV1234

            var customerCode = entity.GetType().GetProperty("EmployeeCode").GetValue(entity);
            if (customerCode != null && customerCode.ToString() != "")
            {
                if (!Regex.IsMatch((string)customerCode, @"^((NV-)(\d+))$"))
                {
                    _serviceResult.Data = new
                    {
                        devMsg = MyResources.ResourcesController.GetMessengerErrorDev("EmployeeCode", "Invalid"," "),
                        userMsg = MyResources.ResourcesController.GetMessengerErrorUser("EmployeeCode", "Invalid"," "),
                    };
                    _serviceResult.StatusCode = 400;
                    return _serviceResult;
                }
            }
            // sử dụng tiếp hàm của base
            return base.Add(entity);
        }

        /// <summary>
        /// xử lí thêm cho cập nhật nhân viên
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author HieuNv
        public override ServiceResult Update(Employee entity, Guid entityId)
        {
            // validate riêng cho employee
            // validate mã nhân viên dạng NV1234

            var customerCode = entity.GetType().GetProperty("EmployeeCode").GetValue(entity);
            if (customerCode != null && customerCode.ToString() != "")
            {
                if (!Regex.IsMatch((string)customerCode, @"^((NV-)(\d+))$"))
                {
                    _serviceResult.Data = new
                    {
                        devMsg = MyResources.ResourcesController.GetMessengerErrorDev("EmployeeCode", "Invalid"," "),
                        userMsg = MyResources.ResourcesController.GetMessengerErrorUser("EmployeeCode", "Invalid"," "),
                    };
                    _serviceResult.StatusCode = 400;
                    return _serviceResult;
                }
            }
            // sử dụng tiếp hàm của base
            return base.Update(entity, entityId);
        }

        /// <summary>
        /// xử lí nghiệp vụ filter phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        /// Author HieuNv
        public ServiceResult GetFilter(int pageSize, int pageNumber, string searchTerms)
        {
            try
            {
                //Xử lí nghiệp vụ
                //Lấy dữ liệu từ db
                PagingResult<Employee> pagingResult = _employeeRepository.GetFilter(pageSize, pageNumber, searchTerms);
                if (pagingResult.Data.Count() > 0)
                {
                    _serviceResult.Data = pagingResult;
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.StatusCode = 500;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MyResources.ResourceErrorType.User,
                };
                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }
    }
}

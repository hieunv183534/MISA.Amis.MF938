using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Interfaces.IServices;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.Amis.Api.Controllers
{
    public class EmployeesController : BaseController<Employee>
    {
        #region Declare

        IEmployeeService _employeeService;

        #endregion

        #region Constructor

        public EmployeesController(IBaseService<Employee> baseService, IEmployeeService employeeService) : base(baseService)
        {
            _employeeService = employeeService;
        }

        #endregion

        /// <summary>
        /// api thêm nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// Author HieuNv
        public override IActionResult Post([FromBody] Employee employee)
        {
            var serviceResult = _employeeService.Add(employee);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }

        /// <summary>
        /// api sử nhân viên
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// Author HieuNv
        [HttpPut("{employeeId}")]
        public override IActionResult Put(Guid employeeId, [FromBody] Employee employee)
        {
            var serviceResult = _employeeService.Update(employee, employeeId);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }

        /// <summary>
        /// api lọc và phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        /// Author HieuNv
        [HttpGet("filter")]
        public IActionResult GetFitler([FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery] string searchTerms)
        {
            var serviceResult = _employeeService.GetFilter(pageSize, pageNumber, searchTerms);
            return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        }


        /// <summary>
        /// Thực hiện xuất fiel excel tất cả các nhân viên
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// Author HieuNv
        [HttpGet("export")]
        public async Task<IActionResult> ExportV2(CancellationToken cancellationToken)
        {
            // query data from database  
            await Task.Yield();

            // gọi lên service để lấy về list customer
            List<Employee> employees = (List<Employee>)_baseService.GetAll().Data;

            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                //Đặt tên người tạo file
                package.Workbook.Properties.Author = "HieuNV";
                //Đặt tiêu đề cho file
                package.Workbook.Properties.Title = "Danh sách nhân viên";
                // tạo sheet 
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                // tạo fontsize và fontfamily cho sheet
                workSheet.Cells.Style.Font.Size = 11;
                workSheet.Cells.Style.Font.Name = "Calibri";

                // danh sách các tên cột
                string[] arrColumnHeader = { "STT" , "Mã nhân viên","Tên nhân viên", "Giới tính", "Ngày sinh",
                    "Chức danh", "Tên đơn vị", "Số tài khoản", "Tên ngân hàng" };



                // merge các column lại từ column 1 đến số column header
                // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                workSheet.Cells[1, 1].Value = "DANH SÁCH NHÂN VIÊN";
                workSheet.Cells[1, 1].Style.Font.Size = 16;
                workSheet.Cells[1, 1, 1, arrColumnHeader.Count()].Merge = true;
                // in đậm
                workSheet.Cells[1, 1, 1, arrColumnHeader.Count()].Style.Font.Bold = true;
                // căn giữa
                workSheet.Cells[1, 1, 1, arrColumnHeader.Count()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                workSheet.Cells[2, 1].Value = "";
                workSheet.Cells[2, 1].Style.Font.Size = 15;
                workSheet.Cells[2, 1, 2, arrColumnHeader.Count()].Merge = true;

                // Gán row header
                for (var i = 0; i < arrColumnHeader.Length; i++)
                {
                    workSheet.Cells[3,i+1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[3,i+1].Style.Border.Left.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#D4D4D4"));
                    workSheet.Cells[3, i + 1].Value = arrColumnHeader[i];
                }
                // chỉnh style cho bảng
                workSheet.Row(3).Style.Font.Bold = true;
                workSheet.Row(3).Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Row(3).Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D8D8D8"));
                workSheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //Chỉnh độ rộng các cột 
                workSheet.Column(1).Width = 5;
                workSheet.Column(2).Width = 15;
                workSheet.Column(3).Width = 25;
                workSheet.Column(4).Width = 15;
                workSheet.Column(5).Width = 15;
                workSheet.Column(6).Width = 25;
                workSheet.Column(7).Width = 25;
                workSheet.Column(8).Width = 15;
                workSheet.Column(9).Width = 25;

                // Gán data list vào sheet
                var rowIndex = 4;
                foreach (var employee in employees)
                {
                    workSheet.Cells[rowIndex, 1].Value = rowIndex-1;
                    workSheet.Cells[rowIndex, 2].Value = employee.EmployeeCode;
                    workSheet.Cells[rowIndex, 3].Value = employee.FullName;
                    workSheet.Cells[rowIndex, 4].Value = 
                        Core.MyResources.ResourceGenderVN.ResourceManager.GetString($"GenderOfValue{employee.Gender}");
                    workSheet.Cells[rowIndex, 5].Value = employee.DateOfBirth ==null ? "":  employee.DateOfBirth?.ToString("dd/MM/yyyy");
                    workSheet.Cells[rowIndex, 6].Value = employee.Position;
                    workSheet.Cells[rowIndex, 7].Value = employee.UnitName;
                    workSheet.Cells[rowIndex, 8].Value = employee.BankAccount;
                    workSheet.Cells[rowIndex, 9].Value = employee.BankName;
                    rowIndex++;
                }
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"EmployeeList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}

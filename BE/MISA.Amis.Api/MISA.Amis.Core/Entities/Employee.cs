using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Entities
{
    public class Employee: BaseEntity
    {
        #region  Field

        private Guid employeeId;
        private string employeeCode;
        private string fullName;
        private DateTime? dateOfBirth;
        private int? gender;
        private Guid? unitId;
        private string unitName;
        private string identityNumber;
        private DateTime? identityDate;
        private string identityPlace;
        private string position;
        private string address;
        private string mobilePhone;
        private string desktopPhone;
        private string email;
        private string bankAccount;
        private string bankName;
        private string bankBranch;

        #endregion

        #region Constructor
        public Employee()
        {

        }
        #endregion

        #region Property

        [Requied]
        //Id nhân viên
        public Guid EmployeeId
        {
            get { return this.employeeId; }
            set { this.employeeId = value; }
        }

        [Requied]
        [NotAllowDuplicate]
        // Mã nhân viên dang NV-1234
        public string EmployeeCode
        {
            get { return this.employeeCode; }
            set { this.employeeCode = value; }
        }

        [Requied]
        // Họ và tên
        public string FullName
        {
            get { return this.fullName; }
            set { this.fullName = value; }
        }

        //Ngày sinh
        public DateTime? DateOfBirth
        {
            get { return this.dateOfBirth; }
            set { this.dateOfBirth = value; }
        }

        // Giới tính: 0=nữ; 1=nam; 2=không xác định
        public int? Gender
        {
            get { return this.gender; }
            set { this.gender = value; }
        }

        [Requied]
        // Id Đơn vị
        public Guid?  UnitId
        {
            get { return this.unitId; }
            set { this.unitId = value; }
        }

        [NotMap]
        // Tên đơn vị
        public string UnitName
        {
            get { return this.unitName; }
            set { this.unitName = value; }
        }

        // Số CMND
        public string IdentityNumber
        {
            get { return this.identityNumber; }
            set { this.identityNumber = value; }
        }

        // Ngày cấp CMND
        public DateTime? IdentityDate
        {
            get { return this.identityDate; }
            set { this.identityDate = value; }
        }

        // Nơi cấp CMND
        public string IdentityPlace
        {
            get { return this.identityPlace; }
            set { this.identityPlace = value; }
        }

        // Chức danh
        public string Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        //Đại chỉ
        public string  Address
        {
            get { return this.address; }
            set { this.address = value; }
        }

        // Số điện thoại di động
        public string MobilePhone
        {
            get { return this.mobilePhone; }
            set { this.mobilePhone = value; }
        }

        //Số điện thoại bàn
        public string DesktopPhone
        {
            get { return this.desktopPhone; }
            set { this.desktopPhone = value; }
        }

        //Email
        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }

        // Tài khoản ngân hàng
        public string BankAccount
        {
            get { return this.bankAccount; }
            set { this.bankAccount = value; }
        }

        //Tên ngân hàng
        public string BankName
        {
            get { return this.bankName; }
            set { this.bankName = value; }
        }

        // Chi nhánh ngân hàng
        public string BankBranch
        {
            get { return this.bankBranch; }
            set { this.bankBranch = value; }
        }

        #endregion

        #region Method

        #endregion
    }
}

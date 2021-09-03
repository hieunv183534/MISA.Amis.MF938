using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Entities
{
    public class Unit : BaseEntity
    {
        #region  Field

        private Guid unitId;
        private string unitCode;
        private string unitName;

        #endregion

        #region Constructor

        public Unit()
        {
                
        }

        #endregion

        #region Property

        //Id đơn vị
        public Guid UnitId
        {
            get { return this.unitId; }
            set { this.unitId = value; }
        }

        // mã đơn vị
        public string UnitCode
        {
            get { return this.unitCode; }
            set { this.unitCode = value; }
        }

        // tên đơn vị
        public string UnitName
        {
            get { return this.unitName; }
            set { this.unitName = value; }
        }

        #endregion
    }
}

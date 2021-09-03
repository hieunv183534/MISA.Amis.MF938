using MISA.Amis.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Interfaces.IServices
{
    public interface IBaseService<MISAEntity>
    {
        /// <summary>
        /// Lấy toàn bộ ds
        /// </summary>
        /// <returns></returns>
        /// Author HieuNv
        ServiceResult GetAll();

        /// <summary>
        /// Lấy theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author HieuNv
        ServiceResult GetById(Guid entityId);

        /// <summary>
        /// Thêm
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Author HieuNv
        ServiceResult Add(MISAEntity entity);

        /// <summary>
        /// Sửa 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author HieuNv
        ServiceResult Update(MISAEntity entity, Guid entityId);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author HieuNv
        ServiceResult Delete(Guid entityId);

        /// <summary>
        /// lấy mã mới
        /// </summary>
        /// <returns></returns>
        /// Author HieuNv
        ServiceResult GetNewCode();

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns></returns>
        ServiceResult DeleteBatch(List<Guid> entityIds);
    }
}

using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Interfaces.IRepositories;
using MISA.Amis.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Services
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {

        #region Declare

        protected IBaseRepository<MISAEntity> _baseRepository;
        public ServiceResult _serviceResult;

        #endregion

        #region Constructor

        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult();
        }

        #endregion

        /// <summary>
        /// Xử lí nghiệp vụ thêm
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Author HieuNv
        public virtual ServiceResult Add(MISAEntity entity)
        {
            try
            {
                // xử lí nghiệp vụ thêm
                var validateMsg = Validate(entity, "add");

                if(validateMsg.devMsg != "-1")
                {
                    _serviceResult.Data = validateMsg;
                    _serviceResult.StatusCode = 400;
                    return _serviceResult;
                }

                // thêm dữ liệu vào db
                var rowAffect = _baseRepository.Add(entity);
                if (rowAffect > 0)
                {
                    _serviceResult.Data = rowAffect;
                    _serviceResult.StatusCode = 201;
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

        /// <summary>
        /// Xử lsi nghiệp vụ xóa
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author HieuNv
        public ServiceResult Delete(Guid entityId)
        {
            try
            {
                // xử lí nghiệp vụ xóa
                // xóa dữ liệu khỏi db
                var rowAffect = _baseRepository.Delete(entityId);
                if (rowAffect > 0)
                {
                    _serviceResult.Data = rowAffect;
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

        /// <summary>
        /// Xử lsi nghiệp vụ lấy dữ liệu
        /// </summary>
        /// <returns></returns>
        /// Author HieuNv
        public ServiceResult GetAll()
        {
            try
            {
                // xử lí nghiệp vụ lấy dữ liệu
                // lấy tất cả dữ liệu từ db
                var entities = _baseRepository.GetAll();
                if (entities.Count() > 0)
                {
                    _serviceResult.Data = entities;
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.StatusCode = 204;
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

        /// <summary>
        /// Xử lí nghiệp vụ lấy bản ghi theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author HieuNv
        public ServiceResult GetById(Guid entityId)
        {
            try
            {
                // xử lí nghiệp vụ lấy 1 dữ liệu
                // lấy bản ghi theo id
                var entity = _baseRepository.GetById(entityId);
                if (entity != null)
                {
                    _serviceResult.Data = entity;
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.StatusCode = 204;
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

        /// <summary>
        /// Xử lsi nghiệp vụ cập nhật bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author HieuNv
        public virtual ServiceResult Update(MISAEntity entity, Guid entityId)
        {
            try
            {
                // xử lí nghiệp vụ thêm
                var validateMsg = Validate(entity, "update");

                if (validateMsg.devMsg != "-1")
                {
                    _serviceResult.Data = validateMsg;
                    _serviceResult.StatusCode = 400;
                    return _serviceResult;
                }

                // thêm dữ liệu vào db
                var rowAffect = _baseRepository.Update(entity,entityId);
                if (rowAffect > 0)
                {
                    _serviceResult.Data = rowAffect;
                    _serviceResult.StatusCode = 201;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="mode"></param>
        /// <returns> messenger lỗi tương ứng </returns>
        /// Author HieuNv
        private MessengerResult Validate(MISAEntity entity, string mode)
        {
            var props = entity.GetType().GetProperties();

            foreach (var prop in props)
            {
                // kiểm tra trường bắt buộc nhập reqiued !!!! 1
                if (prop.IsDefined(typeof(Requied), false))
                {
                    var propValue = prop.GetValue(entity);
                    if (propValue == null)
                    {
                        return new MessengerResult(
                                MyResources.ResourcesController.GetMessengerErrorUser(prop.Name, "Requied", " "),
                                MyResources.ResourcesController.GetMessengerErrorDev(prop.Name, "Requied", " "));
                    }else if(propValue.ToString() == String.Empty) {
                        return new MessengerResult(
                                MyResources.ResourcesController.GetMessengerErrorUser(prop.Name, "Requied", " "),
                                MyResources.ResourcesController.GetMessengerErrorDev(prop.Name, "Requied", " "));
                    }
                    // kiểm tra trường không cho phép trùng !!!! 2
                }
                if (prop.IsDefined(typeof(NotAllowDuplicate), false))
                {
                    var entityDuplicate = _baseRepository.GetByProp(prop.Name, prop.GetValue(entity));
                    if (mode == "add" && entityDuplicate != null)
                    {
                        return new MessengerResult(
                             MyResources.ResourcesController.GetMessengerErrorUser(prop.Name, "NotAllowDuplicate", $" <{prop.GetValue(entity)}> "),
                             MyResources.ResourcesController.GetMessengerErrorDev(prop.Name, "NotAllowDuplicate", $" <{prop.GetValue(entity)}> "));
                    }
                    else if (mode == "update")
                    {
                        if (
                            entityDuplicate.GetType().GetProperty($"{typeof(MISAEntity).Name}Id").GetValue(entityDuplicate).ToString() !=
                            entity.GetType().GetProperty($"{typeof(MISAEntity).Name}Id").GetValue(entity).ToString()
                            )
                        {
                            return new MessengerResult(
                             MyResources.ResourcesController.GetMessengerErrorUser(prop.Name, "NotAllowDuplicate", $" <{prop.GetValue(entity)}> "),
                             MyResources.ResourcesController.GetMessengerErrorDev(prop.Name, "NotAllowDuplicate", $" <{prop.GetValue(entity)}> "));
                        }
                    }
                    // kiểm tra email hợp lệ  !!!! 3
                }
                if (prop.Name == "Email")
                {
                    if(prop.GetValue(entity) != null && prop.GetValue(entity) !="")
                    {
                        if (!Common.IsValidEmail((string)prop.GetValue(entity)))
                        {
                            return new MessengerResult(
                                 MyResources.ResourcesController.GetMessengerErrorUser(prop.Name, "Invalid"," "),
                                 MyResources.ResourcesController.GetMessengerErrorDev(prop.Name, "Invalid"," "));
                        }
                    }
                }
            }
            return new MessengerResult("-1", "-1");
        }

        /// <summary>
        /// xử lí nghiệp vụ lấy mã mới
        /// </summary>
        /// <returns></returns>
        /// Author HieuNv
        public ServiceResult GetNewCode()
        {
            try
            {
                // xử lí nghiệp vụ lấy newcode
                var newCode = _baseRepository.GetNewCode();
                if (newCode != null)
                {
                    _serviceResult.Data = newCode;
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.StatusCode = 204;
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

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns></returns>
        public ServiceResult DeleteBatch(List<Guid> entityIds)
        {
            try
            {
                // xử lí nghiệp vụ xóa
                // xóa dữ liệu khỏi db
                var rowAffect = _baseRepository.DeleteBatch(entityIds);
                if (rowAffect > 0)
                {
                    _serviceResult.Data = rowAffect;
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

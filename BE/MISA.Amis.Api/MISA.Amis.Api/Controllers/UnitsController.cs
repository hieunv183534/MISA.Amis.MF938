using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Amis.Api.Controllers
{
    public class UnitsController : BaseController<Unit>
    {
        public UnitsController(IBaseService<Unit> baseService):base(baseService)
        {
        }
    }
}

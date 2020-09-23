using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Demo.RestServiceHost.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected string UserId => Request.Headers["userId"].FirstOrDefault();

    }
}
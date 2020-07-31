using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("error/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int statuscode)
        {
            return new ObjectResult(new ApiResponse(statuscode));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeoProject.Infrastructure.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiBaseController:ControllerBase
    {
    }
}

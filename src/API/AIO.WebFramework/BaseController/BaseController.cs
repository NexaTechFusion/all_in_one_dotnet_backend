using Microsoft.AspNetCore.Mvc;
using AIO.SharedKernel.Extensions;

namespace AIO.WebFramework.BaseController;


[Route("api/v{version:apiVersion}/[Controller]")]
[ApiController]
public class BaseController : ControllerBase
{
}
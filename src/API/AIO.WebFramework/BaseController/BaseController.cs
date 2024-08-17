using AIO.Application.Shared.DTOs.OperationResult;
using Microsoft.AspNetCore.Mvc;
using AIO.SharedKernel.Extensions;

namespace AIO.WebFramework.BaseController;


[Route("api/v{version:apiVersion}/[Controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    
    protected IActionResult OperationResult<TModel>(OperationResult<TModel> result)
    {

        if (result.Success) return result.Result is bool ? Ok() : Ok(result.Result);

        if (result.IsNotFound)
        {
            ModelState.AddModelError("GeneralError", result.ErrorMessage);

            var notFoundErrors = new ValidationProblemDetails(ModelState);

            return NotFound(notFoundErrors.Errors);
        }

        switch (result.CustomCode)
        {
            case 403:
                ModelState.AddModelError("Forbidden", result.ErrorMessage);
                return StatusCode(result.CustomCode);
            case > 0:
                ModelState.AddModelError("GatewayError", result.ErrorMessage);
                return StatusCode(result.CustomCode);
        }

        return BadRequest(result.ErrorMessage);
    }
}
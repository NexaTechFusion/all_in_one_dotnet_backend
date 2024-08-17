using AIO.Application.Features.Product.Queries.GetList;
using AIO.Application.Shared.DTOs.OperationResult;
using AIO.WebFramework.BaseController;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Mediator;

namespace AIO.Web.API.Controllers.v1.Features.Product;

/// <summary>
/// Product Controller
/// </summary>
/// <param name="mediator"></param>
[ApiVersion("1")]
[Display(Name = "products")]
public class ProductController(IMediator mediator) : BaseController
{
    /// <summary>
    /// Get list of products
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Display(Name = "Get list of products")]
    public async Task<IActionResult> GetListProduct([FromQuery] GetListProductQuery query)
    {
        OperationResult<List<GetListProductQueryResult>> result =
            await mediator.Send(query);
        return OperationResult(result);

        // var productList = new List<Domain.Product.Entities.Product>
        // {
        //     new Domain.Product.Entities.Product()
        //     {
        //         Id = 1,
        //         Name = "Product",
        //         Type = "Product",
        //         Quantity = 10
        //     }
        // };
        // return productList;
    }
}
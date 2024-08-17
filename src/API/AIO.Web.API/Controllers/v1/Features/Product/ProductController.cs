using AIO.WebFramework.BaseController;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AIO.Web.API.Controllers.v1.Features.Product;

[ApiVersion("1")]
[Display(Name = "products")]
public class ProductController : BaseController
{
    public ProductController()
    {
    }


    /// <summary>
    /// Get list of products
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Display(Name = "Get list of products")]
    public List<int> GetProducts()
    {
        var productList = new List<int>
        {
            1,
            2,
            3
        };
        return productList;
    }
}
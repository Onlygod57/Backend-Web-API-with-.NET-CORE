using System.Collections;
using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private IProductCategoryService _productCategoryService;
        private IProductService _productService;
        private readonly IMapper _mapper;

        public ProductCategoriesController(IProductCategoryService productCategoryService, IProductService productService)
        {
            _productCategoryService = productCategoryService;
            _productService = productService;
        }

        [HttpGet("getall")]
        [Authorize(Roles = "random")]

        public IActionResult GetList()
        {
            var result = _productCategoryService.GetList();
            if (result.Success)
                {
                    return Ok(result.Data);
                }
                return BadRequest(result.Message); 
        }

        [HttpGet("getListByName")]
        public IActionResult GetListByName(string Name)
        {
            var result = _productCategoryService.GetListByName(Name);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getListByCategoryType")]
        public IActionResult GetListByCategoryType(string Type)
        {
            var result = _productCategoryService.GetListByCategoryType(Type);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]

        public IActionResult GetById(int Id)
        {
            var result = _productCategoryService.GetById(Id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        [Authorize(Roles = "random")]

        public IActionResult Add(ProductCategory ProductCategory)
        {
            var result = _productCategoryService.Add(ProductCategory);

            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        [Authorize(Roles = "random")]

        public IActionResult Update(ProductCategory ProductCategory)
        {
            var result = _productCategoryService.Update(ProductCategory);

            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        [Authorize(Roles = "random")]
        public IActionResult Delete(int id)
        {
            var result = _productCategoryService.Delete(id);

            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}

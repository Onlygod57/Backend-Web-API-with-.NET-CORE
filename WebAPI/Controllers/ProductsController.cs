using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("add")]
        [Authorize(Roles = "random")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);

            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        [Authorize(Roles = "random")]

        public IActionResult Update(Product product)
        {
            var result = _productService.Update(product);

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
            var result = _productService.Delete(id);

            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getall")]
        [Authorize(Roles = "random")]
        public IActionResult GetList()
        {
            var result = _productService.GetList();
            if(result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getListByProductName")]
        public IActionResult GetListByProductName(string getListByProductName)
        {
            var result = _productService.GetListByProductName(getListByProductName);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getListByQuantityPerUnit")]
        public IActionResult GetListByQuantityPerUnit(int getListByQuantityPerUnit)
        {
            var result = _productService.GetListByQuantityPerUnit(getListByQuantityPerUnit);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getListByUnitsInStock")]
        public IActionResult GetListByUnitsInStock(int unitsInStock)
        {
            var result = _productService.GetListByUnitsInStock(unitsInStock);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getListByPriceRange")]
        public IActionResult GetListByPriceRange(int price1, int price2)
        {
            var result = _productService.GetListByPriceRange(price1, price2);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getListByUnitsOnOrder")]
        public IActionResult GetListByUnitsOnOrder(int unitsOnOrder)
        {
            var result = _productService.GetListByUnitsOnOrder(unitsOnOrder);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getListByCategoryId")]
        public IActionResult GetListByCategoryId(int categoryId)
        {
            var result = _productService.GetListByCategoryId(categoryId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int productId)
        {
            var result = _productService.GetById(productId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        
    }
}

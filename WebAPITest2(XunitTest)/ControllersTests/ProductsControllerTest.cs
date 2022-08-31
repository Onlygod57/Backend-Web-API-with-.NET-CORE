using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;

namespace WebAPITest2_XunitTest_.ControllersTests
{ }
  /*  public class ProductsControllerTest
    {
        [Fact]
        public void ProductShouldBeAbleToBeSuccessfullyAdd()
        {
            //Arrange
            List<Product> _dbProducts = new List<Product>
            {
                new Product { Id = 1, Name ="Laptooop", UnitsOnOrder = 25, Price = 25000, ProductCatalogId = 2, QuantityPerUnit = 5, UnitsInStock = 20},
                new Product { Id = 2, Name = "Mause", UnitsOnOrder = 15, Price = 29000, ProductCatalogId = 3, QuantityPerUnit = 2, UnitsInStock = 20 },
                new Product { Id = 3, Name = "Şarj aleti", UnitsOnOrder = 55, Price = 200, ProductCatalogId = 4, QuantityPerUnit = 91, UnitsInStock = 15 },
                new Product { Id = 4, Name = "Kablo", UnitsOnOrder = 5, Price = 185000, ProductCatalogId = 4, QuantityPerUnit = 70, UnitsInStock = 16 },
                new Product { Id = 5, Name = "Fırın", UnitsOnOrder = 3, Price = 17000, ProductCatalogId = 6, QuantityPerUnit = 91, UnitsInStock = 20 }
            };
            Mock<IProductDal> _mockProductDal = new Mock<IProductDal>();

            IProductService _productService = new ProductManager(_mockProductDal.Object);
            
            var controller = new ProductController(_productService);
            ControllerBase productController = new ProductController(_productService);
            Product newProduct = new Product { Id = 9, Name = "Fırın", UnitsOnOrder = 3, Price = 17000, ProductCatalogId = 6, QuantityPerUnit = 91, UnitsInStock = 19 };
            _mockProductDal.Setup(mr => mr.Add(It.IsAny<Product>())).Callback(
                (Product target) =>
                {
                    _dbProducts.Add(target);
                });
          
            //Act

            var result = (OkObjectResult) controller.Add(newProduct);

            Assert.Equal(result.StatusCode.GetValueOrDefault(), 200);
        }

        [Fact]
        public void ProductShouldBeAbleToBeSuccessfullyUpdate()
        {
            //Arrange
            List<Product> _dbProducts = new List<Product>
            {
                new Product { Id = 1, Name ="Laptooop", UnitsOnOrder = 25, Price = 25000, ProductCatalogId = 2, QuantityPerUnit = 5, UnitsInStock = 20},
                new Product { Id = 2, Name = "Mause", UnitsOnOrder = 15, Price = 29000, ProductCatalogId = 3, QuantityPerUnit = 2, UnitsInStock = 20 },
                new Product { Id = 3, Name = "Şarj aleti", UnitsOnOrder = 55, Price = 200, ProductCatalogId = 4, QuantityPerUnit = 91, UnitsInStock = 15 },
                new Product { Id = 4, Name = "Kablo", UnitsOnOrder = 5, Price = 185000, ProductCatalogId = 4, QuantityPerUnit = 70, UnitsInStock = 16 },
                new Product { Id = 5, Name = "Fırın", UnitsOnOrder = 3, Price = 17000, ProductCatalogId = 6, QuantityPerUnit = 91, UnitsInStock = 20 }
            };
            Mock<IProductDal> _mockProductDal = new Mock<IProductDal>();
            IProductService _productService = new ProductManager(_mockProductDal.Object);
            var controller = new ProductController(_productService);
            Product actual = new Product { Id = 2, Name = "Fırın", UnitsOnOrder = 3, Price = 17000, ProductCatalogId = 6, QuantityPerUnit = 91, UnitsInStock = 19 };

            _mockProductDal.Setup(m => m.Update(It.IsAny<Product>())).Callback(
                (Product target) =>
                {
                    var product = _dbProducts.Where(q => q.Id == target.Id).Single();
                    if (product == null)
                    {
                        throw new InvalidOperationException();
                    }
                    product.QuantityPerUnit = target.QuantityPerUnit;
                    product.UnitsOnOrder = target.UnitsOnOrder;
                    product.Price = target.Price;
                    product.UnitsInStock = target.UnitsInStock;
                    product.Name = target.Name;
                });
            _mockProductDal.Setup(m => m.Get(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.Single(x => x.Id == actual.Id));

            //Act

            var result = (OkObjectResult)controller.Update(actual);

            Assert.Equal(result.StatusCode.GetValueOrDefault(), 200);

        }
    }
}
  */
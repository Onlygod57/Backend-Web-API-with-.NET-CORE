using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;

namespace WebAPITest2.ControllersTests
{
    public class ProductControllersTest:ControllerBase
    {
        
        Mock<IProductDal> _mockProductDal;
        IProductService _productService;
      //  Mock<IProductService> productService;
         List<Product> _dbProducts;

        [Fact]
        public void Start()
        {
            _mockProductDal = new Mock<IProductDal>();
            _productService = new ProductManager(_mockProductDal.Object);
         //   productService = new Mock<IProductService>();
            _dbProducts = new List<Product>
            {
                new Product { Id = 1, Name ="Laptooop", UnitsOnOrder = 25, Price = 25000, ProductCatalogId = 2, QuantityPerUnit = 5, UnitsInStock = 20},
                new Product { Id = 2, Name = "Mause", UnitsOnOrder = 15, Price = 29000, ProductCatalogId = 3, QuantityPerUnit = 2, UnitsInStock = 20 },
                new Product { Id = 3, Name = "Şarj aleti", UnitsOnOrder = 55, Price = 200, ProductCatalogId = 4, QuantityPerUnit = 91, UnitsInStock = 15 },
                new Product { Id = 4, Name = "Kablo", UnitsOnOrder = 5, Price = 185000, ProductCatalogId = 4, QuantityPerUnit = 70, UnitsInStock = 16 },
                new Product { Id = 5, Name = "Fırın", UnitsOnOrder = 3, Price = 17000, ProductCatalogId = 6, QuantityPerUnit = 91, UnitsInStock = 20 }
            };

            //Birden fazla fonksiyonda kullanıldığı için bu setup'ı gloablde tanımalamak istedim.
            _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts);
        }

        [TestMethod]
        public void ProductShouldBeAbleToBeSuccessfullyAdd()
        {
            //Arrange
            var controller = new ProductController(_productService);
                ControllerBase productController = new ProductController(_productService);
                Product newProduct = new Product { Id = 9, Name = "Fırın", UnitsOnOrder = 3, Price = 17000, ProductCatalogId = 6, QuantityPerUnit = 91, UnitsInStock = 19 };
                _mockProductDal.Setup(mr => mr.Add(It.IsAny<Product>())).Callback(
                    (Product target) =>
                    {
                        _dbProducts.Add(target);
                    });

            //Act

            ViewResult result = controller.Add(newProduct) as ViewResult;

            var expe = controller.Add(newProduct);
            var actual = controller.GetList();

            //productService.Add(newProduct);
            // var expected = productService.GetList().Data.Count;

            //Assert
            Assert.AreEqual("Add", result.ViewName);
        //    Assert.IsInstanceOfType(actual, typeof(Product));
        }
    }
}
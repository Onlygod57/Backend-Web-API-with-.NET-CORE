using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Concrete;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;

namespace WebAPITest2.ControllersTests
{
    [TestClass]
    public class ProductControllersTest : ControllerBase
    {
        //  Projede Business kısmı test edildiği için bu bölümde Businessları Mock ile tanımlayarak projenin test aşamasındaki süre kaybının önlenmesini istedim.   //
        Mock<IProductService> _mockProductService;
        List<Product> _dbProducts;
        [TestInitialize]
        public void Start()
        {
            _mockProductService = new Mock<IProductService>();
            _dbProducts = new List<Product>
            {
                new Product { Id = 1, Name ="Laptooop", UnitsOnOrder = 25, Price = 25000, ProductCatalogId = 2, QuantityPerUnit = 5, UnitsInStock = 20},
                new Product { Id = 2, Name = "Mause", UnitsOnOrder = 15, Price = 29000, ProductCatalogId = 3, QuantityPerUnit = 2, UnitsInStock = 20 },
                new Product { Id = 3, Name = "Şarj aleti", UnitsOnOrder = 55, Price = 200, ProductCatalogId = 4, QuantityPerUnit = 91, UnitsInStock = 15 },
                new Product { Id = 4, Name = "Kablo", UnitsOnOrder = 5, Price = 185000, ProductCatalogId = 4, QuantityPerUnit = 70, UnitsInStock = 16 },
                new Product { Id = 5, Name = "Fırın", UnitsOnOrder = 3, Price = 17000, ProductCatalogId = 6, QuantityPerUnit = 91, UnitsInStock = 20 }
            };

            //Birden fazla fonksiyonda kullanıldığı için bu setup'ı gloablde tanımalamak istedim.
            _mockProductService.Setup(m => m.GetList()).Returns(new IResult<List<Product>>(_dbProducts));
        }

        [TestMethod]
        public void ProductShouldBeAbleToBeSuccessfullyAddAndReturn200StatusCode()
        {
            //Arrange
            var controller = new ProductController(_mockProductService.Object);
            Product newProduct = new Product { Id = 9, Name = "Fırın", UnitsOnOrder = 3, Price = 17000, ProductCatalogId = 6, QuantityPerUnit = 91, UnitsInStock = 19 };
            _mockProductService.Setup(pm => pm.Add(It.IsAny<Product>())).Returns(new SuccessResult(Messages.ProductAdded));

            //Act
            var result = (OkObjectResult)controller.Add(newProduct);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductShouldBeAbleToBeSuccessfullyUpdateAndReturn200StatusCode()
        {
            var controller = new ProductController(_mockProductService.Object);
            Product updatedProduct = new Product { Id = 2, Name = "Fırın", UnitsOnOrder = 3, Price = 17000, ProductCatalogId = 6, QuantityPerUnit = 91, UnitsInStock = 19 };
            _mockProductService.Setup(pm => pm.Update(It.IsAny<Product>())).Returns(new SuccessResult(Messages.ProductUpdated));

            //Act
            var result = (OkObjectResult)controller.Update(updatedProduct);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductShouldBeAbleToBeSuccessfullyDeleteAndReturn200StatusCode()
        {
            //Arrange
            var controller = new ProductController(_mockProductService.Object);
            var testId = 3;
            _mockProductService.Setup(m => m.Delete(It.IsAny<int>())).Returns(new SuccessResult(Messages.ProductDeleted));

            //Act
            var result = (OkObjectResult)controller.Delete(testId);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductShouldBeAbleToBeSuccessfullyGetListAndReturn200StatusCode()
        {
            //Arrange
            var controller = new ProductController(_mockProductService.Object);

            //Act
            var result = (OkObjectResult)controller.GetList();

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductShouldToBeSuccessfullyGetListByProductNameAndReturn200StatusCode()

        {
            //Arrange
            var controller = new ProductController(_mockProductService.Object);
            string name = "mause";
            _mockProductService.Setup(pm => pm.GetListByProductName(It.IsAny<string>())).Returns(new IResult<List<Product>>(_dbProducts.FindAll(x => x.Name == name)));

            //Act
            var result = (OkObjectResult)controller.GetListByProductName(name);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductShouldToBeSuccessfullyGetListByQuantityPerUnitAndReturn200StatusCode()
        {
            //Arrange
            var controller = new ProductController(_mockProductService.Object);
            short quantityPerUnit = 91;
            _mockProductService.Setup(pm => pm.GetListByQuantityPerUnit(It.IsAny<int>()))
                .Returns(new IResult<List<Product>>(_dbProducts.FindAll(x => x.QuantityPerUnit == quantityPerUnit)));

            //Act
            var result = (OkObjectResult)controller.GetListByQuantityPerUnit(quantityPerUnit);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductShouldToBeSuccessfullyGetListByUnitsInStockAndReturn200StatusCode()
        {
            //Arrange
            var controller = new ProductController(_mockProductService.Object);
            short unitsInStock = 20;
            _mockProductService.Setup(pm => pm.GetListByUnitsInStock(It.IsAny<int>()))
                .Returns(new IResult<List<Product>>(_dbProducts.FindAll(x => x.UnitsInStock == unitsInStock)));

            //Act
            var result = (OkObjectResult)controller.GetListByUnitsInStock(unitsInStock);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductShouldToBeSuccessfullyGetListByPriceRangeAndReturn200StatusCode()
        {
            //Arrange
            var controller = new ProductController(_mockProductService.Object);
            int price1 = 24000;
            int price2 = 29000;
            _mockProductService.Setup(pm => pm.GetListByPriceRange(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new IResult<List<Product>>(_dbProducts.FindAll(d => d.Price > price1 && d.Price < price2)));

            //Act
            var result = (OkObjectResult)controller.GetListByPriceRange(price1, price2);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductShouldToBeSuccessfullyGetListByUnitsOnOrderAndReturn200StatusCode()
        {
            //Arrange
            var controller = new ProductController(_mockProductService.Object);
            short unitsOnOrder = 15;
            _mockProductService.Setup(pm => pm.GetListByUnitsOnOrder(It.IsAny<int>()))
                .Returns(new IResult<List<Product>>(_dbProducts.FindAll(x => x.UnitsOnOrder == unitsOnOrder)));

            //Act
            var result = (OkObjectResult)controller.GetListByUnitsOnOrder(unitsOnOrder);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductShouldBeAbleToBeSuccessfullyGetListByCategoryIdAndReturn200StatusCode()
        {
            //Arrange
            var controller = new ProductController(_mockProductService.Object);
            var categoryId = 4;
            _mockProductService.Setup(pm => pm.GetListByCategoryId(It.IsAny<int>()))
                .Returns(new IResult<List<Product>>(_dbProducts.FindAll(x => x.ProductCatalogId == categoryId)));

            //Act
            var result = (OkObjectResult)controller.GetListByCategoryId(categoryId);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductShouldBeAbleToBeSuccessfullyGetByIdAndReturn200StatusCode()
        {
            //Arrange
            var testId = 2;
            var controller = new ProductController(_mockProductService.Object);
            _mockProductService.Setup(pm => pm.GetById(It.IsAny<int>()))
                .Returns(new IResult<Product>(_dbProducts.SingleOrDefault(x => x.Id == testId)));

            //Act
            var result = (OkObjectResult)controller.GetById(testId);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }
    }
}
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

namespace WebAPITest2.ControllersTests
{
    [TestClass]
    public class ProductControllersTest : ControllerBase
    {
        Mock<IProductDal> _mockProductDal;
        IProductService _productService;
        //  Mock<IProductService> productService;
        List<Product> _dbProducts;
        [TestInitialize]
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
            var result = (OkObjectResult)controller.Add(newProduct);

            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductShouldBeAbleToBeSuccessfullyUpdate()
        {
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

            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void ProductShouldBeAbleToBeSuccessfullyDelete()
        {
            var controller = new ProductController(_productService);
            var testId = 3;
            IProductService productService = new ProductManager(_mockProductDal.Object);

            _mockProductDal.Setup(m => m.Delete(It.IsAny<Product>())).Callback(
                   (Product target) =>
                   {
                       _dbProducts.Remove(target);
                   });

            _mockProductDal.Setup(m => m.Get(p => p.Id == testId)).Returns(_dbProducts.Single(x => x.Id == testId));

            //Act
            var result = (OkObjectResult)controller.Delete(testId);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductShouldBeAbleToBeSuccessfullyGetList()
        {
            //Arrange
            IProductService productService = new ProductManager(_mockProductDal.Object);
            var controller = new ProductController(_productService);

            //Act
            var result = (OkObjectResult)controller.GetList();
            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductShouldToBeSuccessfullyGetListByProductName()

        {
            //Arrange
            IProductService productService = new ProductManager(_mockProductDal.Object);
            var controller = new ProductController(_productService);
            string name = "mause";

            _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(x => x.Name == name));

            //Act
            var result = (OkObjectResult)controller.GetListByProductName(name);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductShouldToBeSuccessfullyGetListByQuantityPerUnit()
        {
            //Arrange
            IProductService productService = new ProductManager(_mockProductDal.Object);
            var controller = new ProductController(_productService);
            short quantityPerUnit = 91;

            _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(x => x.QuantityPerUnit == quantityPerUnit));

            //Act
            var result = (OkObjectResult)controller.GetListByQuantityPerUnit(quantityPerUnit);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductShouldToBeSuccessfullyGetListByUnitsInStock()
        {
            //Arrange
            IProductService productService = new ProductManager(_mockProductDal.Object);
            var controller = new ProductController(_productService);
            short unitsInStock = 20;

            _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(x => x.UnitsInStock == unitsInStock));

            //Act
            var result = (OkObjectResult)controller.GetListByUnitsInStock(unitsInStock);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductShouldToBeSuccessfullyGetListByPriceRange()
        {
            //Arrange
            IProductService productService = new ProductManager(_mockProductDal.Object);
            var controller = new ProductController(_productService);
            int price1 = 24000;
            int price2 = 29000;
            _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(d => d.Price > price1 && d.Price < price2));

            //Act
            var result = (OkObjectResult)controller.GetListByPriceRange(price1, price2);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }
        
        [TestMethod]
        public void ProductShouldToBeSuccessfullyGetListByUnitsOnOrder()
        {
            //Arrange
            IProductService productService = new ProductManager(_mockProductDal.Object);
            var controller = new ProductController(_productService);
            short unitsOnOrder = 15;
            _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(p => p.UnitsOnOrder == unitsOnOrder));

            //Act
            var result = (OkObjectResult)controller.GetListByUnitsOnOrder(unitsOnOrder);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductShouldBeAbleToBeSuccessfullyGetListByCategoryId()
        {
            //Arrange
            IProductService productService = new ProductManager(_mockProductDal.Object);
            var controller = new ProductController(_productService);
            var categoryId = 4;
            _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(x => x.ProductCatalogId == categoryId));

            //Act
            var result =(OkObjectResult)controller.GetListByCategoryId(categoryId);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductShouldBeAbleToBeSuccessfullyGetById()
        {
            //Arrange
            var testId = 2;
            IProductService productService = new ProductManager(_mockProductDal.Object);
            var controller = new ProductController(_productService);
            _mockProductDal.Setup(m => m.Get(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.Single(x => x.Id == testId));
            
            //Act
            var result = (OkObjectResult)controller.GetById(testId);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }
    }
}
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
using Moq;

namespace WebAPITest2.BusinessTests
{
    [TestClass]
    public class ProductManagerTest
    {
        Mock<IProductDal> _mockProductDal;

        List<Product> _dbProducts;
        [TestInitialize]
        public void Start()
        {
            _mockProductDal = new Mock<IProductDal>();
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
        public void ProductShouldBeAbleToBeSuccessfullyGetById()
        {
            //Arrange
            var testId = 2;
            _mockProductDal.Setup(m => m.Get(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.Single(x => x.Id == testId));
            IProductService productService = new ProductManager(_mockProductDal.Object);

            //Act
            IDataResult<Product> products = productService.GetById(testId);
            var result = products.Data;

            //Assert
            Assert.AreEqual(result.Id, testId);
        }

        [TestMethod]
        public void ProductShouldBeAbleToBeSuccessfullyGetList()
        {
            //Arrange
            IProductService productService = new ProductManager(_mockProductDal.Object);
            
            //Act
            IDataResult<List<Product>> products = productService.GetList();

            //Assert
            Assert.AreEqual(5, products.Data.Count);
        }

        [TestMethod]
        public void ProductShouldBeAbleToBeSuccessfullyGetListByUnitsOnOrder()
        {
            //Arrange
            IProductService productService = new ProductManager(_mockProductDal.Object);
            short testUnitsOnOrder = 91;
            _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(x => x.UnitsOnOrder == testUnitsOnOrder));
            
            //Act
            IDataResult<List<Product>> products = productService.GetListByUnitsOnOrder(testUnitsOnOrder);
            var result = products.Data;

            //Assert
            foreach (var product in result)
            {
                Assert.AreEqual(product.UnitsOnOrder, testUnitsOnOrder);
            }
        }

        [TestMethod]
        public void ProductShouldBeAbleToBeSuccessfullyGetListByQuantityPerUnitTest()
        {
            //Arrange
            IProductService productService = new ProductManager(_mockProductDal.Object);
            int testQuantityPerUnit = 91;
            _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(x => x.QuantityPerUnit == testQuantityPerUnit));
            
            //Act
            IDataResult<List<Product>> products = productService.GetListByQuantityPerUnit(testQuantityPerUnit);
            var result = products.Data;

            //Assert
            foreach (var product in result)
            {
                Assert.AreEqual(product.QuantityPerUnit, testQuantityPerUnit);
            }
        }

        [TestMethod]
        public void ProductShouldBeAbleToBeSuccessfullyGetListByUnitsInStockTest()
        {
            //Arrange
            IProductService productService = new ProductManager(_mockProductDal.Object);
            int testUnitsInStock = 20;
            _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(x => x.UnitsInStock == testUnitsInStock));

            //Act
            IDataResult<List<Product>> products = productService.GetListByUnitsInStock(testUnitsInStock);
            var result = products.Data;

            //Assert
            foreach (var product in result)
            {
                Assert.AreEqual(product.UnitsInStock, testUnitsInStock);
            }
        }

        [TestMethod]
        public void ProductShouldBeAbleToBeSuccessfullyGetListByProductNameTest()
        {
            //Arrange
            IProductService productService = new ProductManager(_mockProductDal.Object);
            string name = "mause";
            _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(x => x.Name == name));

            //Act
            IDataResult<List<Product>> products = productService.GetListByProductName(name);
            var result = products.Data;

            //Assert
            foreach (var product in result)
            {
                Assert.AreEqual(product.Name, name);
            }
        }

        [TestMethod]
        public void ProductShouldBeAbleToBeSuccessfullyGetListByPriceRange()
        {
            //Arrange
            IProductService productService = new ProductManager(_mockProductDal.Object);
            int price1 = 24000;
            int price2 = 30000;
            _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(d => d.Price > price1 && d.Price < price2));

            //Act
            IDataResult<List<Product>> products = productService.GetListByPriceRange(price1, price2);

            var result = products.Data;

            //Assert
            if (result.Count == 0)
            {
                Assert.IsTrue(false);
            }
            else
                foreach (var product in result)
                {
                    Assert.IsTrue(price1 < product.Price && price2 > product.Price);
                }

        }

        [TestMethod]
        public void ProductShouldBeAbleToBeSuccessfullyGetListByCategoryId()
        {
            //Arrange
            IProductService productService = new ProductManager(_mockProductDal.Object);
            var categoryId = 4;
            _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(x => x.ProductCatalogId == categoryId));

            //Act
            IDataResult<List<Product>> products = productService.GetListByCategoryId(categoryId);
            var result = products.Data;
            

            //Assert
            foreach (var product in result)
            {
                Assert.AreEqual(product.ProductCatalogId, categoryId);
            }
        }


        [TestMethod]
        public void ProductShouldBeAbleToBeSuccessfullyDeleted()
        {
            //Arrange
            var testId = 3;
            IProductService productService = new ProductManager(_mockProductDal.Object);

            _mockProductDal.Setup(m => m.Delete(It.IsAny<Product>())).Callback(
                   (Product target) =>
                   {
                       _dbProducts.Remove(target);
                   });

            _mockProductDal.Setup(m => m.Get(p => p.Id == testId)).Returns(_dbProducts.Single(x => x.Id == testId));

            //Act
            var actual = productService.GetList().Data.Count();
            productService.Delete(testId);
            var expected = productService.GetList().Data.Count() + 1;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProductShouldBeAbleToBeSuccessfullyAdded()
        {
            //Arrange
            IProductService productService = new ProductManager(_mockProductDal.Object);
            Product newProduct = new Product { Id = 9, Name = "Fırın", UnitsOnOrder = 3, Price = 17000, ProductCatalogId = 6, QuantityPerUnit = 91, UnitsInStock = 19 };
            _mockProductDal.Setup(mr => mr.Add(It.IsAny<Product>())).Callback(
                (Product target) =>
                {
                    _dbProducts.Add(target);
                });

            //Act
            var actual = productService.GetList().Data.Count + 1;
            productService.Add(newProduct);
            var expected = productService.GetList().Data.Count;
            
            //Assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void ProductShouldBeAbleToBeSuccessfullyUpdated()
        {
            //Arrange
            IProductService productService = new ProductManager(_mockProductDal.Object);
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
            productService.Update(actual);
            IDataResult<Product> expect = productService.GetById(actual.Id);
            var result = expect.Data;

            //Assert
            Assert.AreEqual(result.Id, actual.Id);

        }
    }
}

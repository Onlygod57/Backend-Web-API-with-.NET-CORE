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
using Entities.Dtos;
using Moq;

namespace WebAPITest2.BusinessTests
{

    [TestClass]
    public class ProductCategoryManagerTest
    {
        Mock<IProductCategoryDal> _mockProductCategoryDal;
        Mock<IProductDal> _mockProductDal;

        List<ProductCategory> _dbProductCategories;
        List<Product> _dbProducts;

        [TestInitialize]

        public void Start()
        {
            _mockProductCategoryDal = new Mock<IProductCategoryDal>();
            _mockProductDal = new Mock<IProductDal>();
            _dbProductCategories = new List<ProductCategory>
            {
                new ProductCategory { Id = 1, CategoryType = "Tabak", Description = "Bu bir Porselen türüdür.", Name ="Porselen"},
                new ProductCategory { Id = 2, CategoryType = "Teknolojik ürün", Description = "Bu bir teknolojik türüdür.", Name ="Teknolojik Ürün"},
                new ProductCategory { Id = 3, Name = "Beyaz Eşya", Description = "Bu bir Beyaz eşya türüdür.", CategoryType = "Beyaz Eşya"},
                new ProductCategory { Id = 4, Name = "Elektik Ürnüleri", Description = "Bu bir elektrik türüdür.", CategoryType = "Elektrik Ürün" }
            };
            _dbProducts = new List<Product>

            {
                new Product { Id = 54, Name = "Çamaşır Makinesi", UnitsOnOrder = 12, Price = 23000, ProductCatalogId = 3, QuantityPerUnit = 21, UnitsInStock = 10 },
                new Product { Id = 55, Name = "Bulaşık Makinesi", UnitsOnOrder = 3, Price = 17000, ProductCatalogId = 3, QuantityPerUnit = 91, UnitsInStock = 20 },
                new Product { Id = 45, Name = "Kablo", UnitsOnOrder = 12, Price = 23000, ProductCatalogId = 4, QuantityPerUnit = 21, UnitsInStock = 10 },
                new Product { Id = 65, Name = "Şarj aleti", UnitsOnOrder = 3, Price = 17000, ProductCatalogId = 4, QuantityPerUnit = 91, UnitsInStock = 20 },
                new Product { Id = 25, Name = "Bilgisayar", UnitsOnOrder = 12, Price = 23000, ProductCatalogId = 2, QuantityPerUnit = 21, UnitsInStock = 10 },
                new Product { Id = 35, Name = "Fırın", UnitsOnOrder = 3, Price = 17000, ProductCatalogId = 2, QuantityPerUnit = 91, UnitsInStock = 20 },
                new Product { Id = 5, Name = "Porselen", UnitsOnOrder = 3, Price = 17000, ProductCatalogId = 1, QuantityPerUnit = 91, UnitsInStock = 20 },
                new Product { Id = 15, Name = "Çay tabağı", UnitsOnOrder = 18, Price = 15000, ProductCatalogId = 1, QuantityPerUnit = 25, UnitsInStock = 18 },

            };

            _mockProductCategoryDal.Setup(m => m.GetList(It.IsAny<Expression<Func<ProductCategory, bool>>>())).Returns(_dbProductCategories);

        }
        [TestMethod]
        public void ProductCategoryShouldBeAbleToBeSuccessfullyGetById()
        {
            //Arrange
            var testId = 2;
            _mockProductCategoryDal.Setup(m => m.Get(It.IsAny<Expression<Func<ProductCategory, bool>>>())).Returns(_dbProductCategories.Single(x => x.Id == testId));
            IProductCategoryService productCategoryService = new ProductCategoryManager(_mockProductCategoryDal.Object, _mockProductDal.Object);

            //Act
            IDataResult<ProductCategory> productCategories = productCategoryService.GetById(testId);
            var result = productCategories.Data;

            //Assert
            Assert.AreEqual(result.Id, testId);
        }


        [TestMethod]
        public void ProductCategoryShouldBeAbleToBeSuccessfullyGetList()
        {
            //Arrange
            IProductCategoryService productCategoryService = new ProductCategoryManager(_mockProductCategoryDal.Object, _mockProductDal.Object);
            foreach (var productCategory in _dbProductCategories)
            {
                _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(x => x.Id == productCategory.Id));
                _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(x => x.Id == productCategory.Id));
                _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(x => x.Id == productCategory.Id));
                _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(x => x.Id == productCategory.Id));
            }

            //Act
            IDataResult<List<ProductCategoryDto>> productCategories = productCategoryService.GetList();

            //Assert
            Assert.AreEqual(4, productCategories.Data.Count);
        }

        [TestMethod]
        public void ProductCategoryShouldBeAbleToBeSuccessfullyGetListByName()
        {
            //Arrange
            IProductCategoryService productCategoryService = new ProductCategoryManager(_mockProductCategoryDal.Object, _mockProductDal.Object);
            string name = "Beyaz Eşya";
            _mockProductCategoryDal.Setup(m => m.GetList(It.IsAny<Expression<Func<ProductCategory, bool>>>())).Returns(_dbProductCategories.FindAll(pc => pc.Name == name));

            //Act
            IDataResult<List<ProductCategory>> productCategories = productCategoryService.GetListByName(name);
            var result = productCategories.Data;

            //Assert
            if (result.Count == 0)
            {
                Assert.IsTrue(false);
            }
            else
                foreach (var product in result)
                {
                    Assert.AreEqual(product.Name, name);
                }
        }

        [TestMethod]
        public void ProductCategoryShouldBeAbleToBeSuccessfullyGetListByCategoryType()
        {
            //Arrange
            IProductCategoryService productCategoryService = new ProductCategoryManager(_mockProductCategoryDal.Object, _mockProductDal.Object);
            string categoryType = "Beyaz Eşya";
            _mockProductCategoryDal.Setup(m => m.GetList(It.IsAny<Expression<Func<ProductCategory, bool>>>())).Returns(_dbProductCategories.FindAll(pc => pc.CategoryType == categoryType));

            //Act
            IDataResult<List<ProductCategory>> productCategories = productCategoryService.GetListByName(categoryType);
            var result = productCategories.Data;

            //Assert
            if (result.Count == 0)
            {
                Assert.IsTrue(false);
            }
            else
                foreach (var product in result)
                {
                    Assert.AreEqual(product.Name, categoryType);
                }
        }


        [TestMethod]
        public void ProductCategoryShouldBeAbleToBeSuccessfullyAdded()
        {
            //Arrange
            IProductCategoryService productCategoryService = new ProductCategoryManager(_mockProductCategoryDal.Object, _mockProductDal.Object);

            foreach (var productCategory in _dbProductCategories)
            {
                _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(x => x.Id == productCategory.Id));
                _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(x => x.Id == productCategory.Id));
                _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(x => x.Id == productCategory.Id));
                _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(x => x.Id == productCategory.Id));
            }

            ProductCategory newProductCategory = new ProductCategory { Id = 5, Name = "Çip", CategoryType = "Çip", Description = "Bu ürünler Çip türündedir." };
            _mockProductCategoryDal.Setup(mr => mr.Add(It.IsAny<ProductCategory>())).Callback(
                (ProductCategory target) =>
                {
                    _dbProductCategories.Add(target);
                });

            //Act
            var actual = productCategoryService.GetList().Data.Count + 1;
            productCategoryService.Add(newProductCategory);
            var expected = productCategoryService.GetList().Data.Count;

            //Assert
            Assert.AreEqual(actual, expected);
        }


        [TestMethod]
        public void ProductCategoryShouldBeAbleToBeSuccessfullyUpdated()
        {
            //Arrange
            IProductCategoryService productCategoryService = new ProductCategoryManager(_mockProductCategoryDal.Object, _mockProductDal.Object);
            ProductCategory actual = new ProductCategory
            {
                Id = 4,
                Name = "Electrical Products",
                Description = "Bu bir elektrik türüdür.",
                CategoryType = "Elektrik Ürün"
            };

            _mockProductCategoryDal.Setup(m => m.Update(It.IsAny<ProductCategory>())).Callback(
                (ProductCategory target) =>
                {
                    var productCategory = _dbProductCategories.Where(q => q.Id == target.Id).Single();
                    if (productCategory == null)
                    {
                        throw new InvalidOperationException();
                    }
                    productCategory.Name = target.Name;
                    productCategory.CategoryType = target.CategoryType;
                    productCategory.Description = target.Description;
                });
            _mockProductCategoryDal.Setup(m => m.Get(It.IsAny<Expression<Func<ProductCategory, bool>>>())).Returns(_dbProductCategories.Single(x => x.Id == actual.Id));

            //Act
            productCategoryService.Update(actual);
            IDataResult<ProductCategory> expect = productCategoryService.GetById(actual.Id);
            var result = expect.Data;

            //Assert
            Assert.AreEqual(result.Id, actual.Id);

        }

        [TestMethod]
        public void ProductCategoryShouldBeAbleToBeSuccessfullyDeleted()
        {
            //Arrange
            var testId = 3;
            IProductCategoryService productCategoryService = new ProductCategoryManager(_mockProductCategoryDal.Object, _mockProductDal.Object);

            _mockProductCategoryDal.Setup(m => m.Delete(It.IsAny<ProductCategory>())).Callback(
                   (ProductCategory target) =>
                   {
                       _dbProductCategories.Remove(target);
                   });
            foreach (var productCategory in _dbProductCategories)
            {
                _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(x => x.Id == productCategory.Id));
                _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(x => x.Id == productCategory.Id));
                _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(x => x.Id == productCategory.Id));
                _mockProductDal.Setup(m => m.GetList(It.IsAny<Expression<Func<Product, bool>>>())).Returns(_dbProducts.FindAll(x => x.Id == productCategory.Id));
            }
            _mockProductCategoryDal.Setup(m => m.Get(p => p.Id == testId)).Returns(_dbProductCategories.Single(x => x.Id == testId));

            //Act
            var actual = productCategoryService.GetList().Data.Count();
            productCategoryService.Delete(testId);
            var expected = productCategoryService.GetList().Data.Count() + 1;

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}

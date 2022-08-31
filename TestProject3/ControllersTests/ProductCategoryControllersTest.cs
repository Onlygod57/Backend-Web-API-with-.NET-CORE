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
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;

namespace TestProject3.ControllersTests
{
    [TestClass]
    public class ProductCategoryControllersTest
    {
        //Projede Business kısmı test edildiği için bu bölümde Businessları Mock ile tanımlayarak projenin test aşamasındaki süre kaybının önlenmesini istedim.
        Mock<IProductService> _mockProductService;
        Mock<IProductCategoryService> _mockProductCategoryService;

        List<ProductCategory> _dbProductCategories;
        List<Product> _dbProducts;

        [TestInitialize]

        public void Start()
        {

            _mockProductCategoryService = new Mock<IProductCategoryService>();
            _mockProductService = new Mock<IProductService>();

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

            List<ProductCategoryDto> productCategoryDtos = new List<ProductCategoryDto>
            {
                new ProductCategoryDto { Id = 1, CategoryType = "Tabak", Description = "Bu bir Porselen türüdür.", Name ="Porselen", Products = null},
                new ProductCategoryDto { Id = 2, CategoryType = "Teknolojik ürün", Description = "Bu bir teknolojik türüdür.", Name ="Teknolojik Ürün", Products = null},
                new ProductCategoryDto { Id = 3, Name = "Beyaz Eşya", Description = "Bu bir Beyaz eşya türüdür.", CategoryType = "Beyaz Eşya", Products = null},
                new ProductCategoryDto { Id = 4, Name = "Elektik Ürnüleri", Description = "Bu bir elektrik türüdür.", CategoryType = "Elektrik Ürün", Products = null }
            };

            _mockProductCategoryService.Setup(pm => pm.GetList()).Returns(new IResult<List<ProductCategoryDto>>(productCategoryDtos));
        }

        [TestMethod]
        public void ProductCategoryShouldBeAbleToBeSuccessfullyGetListAndReturn200StatusCode()
        {
            //Arrange
            var controller = new ProductCategoriesController(_mockProductCategoryService.Object, _mockProductService.Object);

            //Act
            var result = (OkObjectResult)controller.GetList();

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductCategoryShouldBeAbleToBeSuccessfullyGetListByNameAndReturn200StatusCode()
        {
            //Arrange
            var controller = new ProductCategoriesController(_mockProductCategoryService.Object, _mockProductService.Object);
            string name = "Beyaz Eşya";
            _mockProductCategoryService.Setup(pm => pm.GetListByName(It.IsAny<string>()))
                .Returns(new IResult<List<ProductCategory>>(_dbProductCategories.FindAll(x => x.Name == name)));

            //Act
            var result = (OkObjectResult)controller.GetListByName(name);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductCategoryShouldBeAbleToBeSuccessfullyGetListByCategoryTypeAndReturn200StatusCode()
        {
            //Arrange
            var controller = new ProductCategoriesController(_mockProductCategoryService.Object, _mockProductService.Object);
            string categoryType = "Beyaz Eşya";
            _mockProductCategoryService.Setup(m => m.GetListByCategoryType(It.IsAny<string>()))
                .Returns(new IResult<List<ProductCategory>>(_dbProductCategories.FindAll(pc => pc.CategoryType == categoryType)));

            //Act
            var result = (OkObjectResult)controller.GetListByCategoryType(categoryType);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductCategoryShouldBeAbleToBeSuccessfullyAddedAndReturn200StatusCode()
        {
            //Arrange
            var controller = new ProductCategoriesController(_mockProductCategoryService.Object, _mockProductService.Object);

            ProductCategory newProductCategory = new ProductCategory { Id = 5, Name = "Çip", CategoryType = "Çip", Description = "Bu ürünler Çip türündedir." };
            _mockProductCategoryService.Setup(pm => pm.Add(It.IsAny<ProductCategory>()))
                .Returns(new SuccessResult(Messages.ProductAdded));

            //Act
            var result = (OkObjectResult)controller.Add(newProductCategory);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductCategoryShouldBeAbleToBeSuccessfullyUpdatedAndReturn200StatusCode()
        {
            //Arrange
            var controller = new ProductCategoriesController(_mockProductCategoryService.Object, _mockProductService.Object);
            ProductCategory updatedProductCategory = new ProductCategory
            {
                Id = 4,
                Name = "Electrical Products",
                Description = "Bu bir elektrik türüdür.",
                CategoryType = "Elektrik Ürün"
            };
            _mockProductCategoryService.Setup(pm => pm.Update(It.IsAny<ProductCategory>())).Returns(new SuccessResult(Messages.ProductUpdated));

            //Act
            var result = (OkObjectResult)controller.Update(updatedProductCategory);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductCategoryShouldBeAbleToBeSuccessfullyDeletedAndReturn200StatusCode()
        {
            //Arrange
            var controller = new ProductCategoriesController(_mockProductCategoryService.Object, _mockProductService.Object);
            var testId = 3;
            _mockProductCategoryService.Setup(m => m.Delete(It.IsAny<int>())).Returns(new SuccessResult(Messages.ProductDeleted));

            //Act
            var result = (OkObjectResult)controller.Delete(testId);

            //Assert
            Assert.AreEqual(result.StatusCode.GetValueOrDefault(), 200);
            Assert.IsNotNull(result);
        }
    }
}

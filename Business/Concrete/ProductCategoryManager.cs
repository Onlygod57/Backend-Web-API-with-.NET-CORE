using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class ProductCategoryManager : IProductCategoryService
    {
        private IProductCategoryDal _productCategoryDal;
        private IProductDal _productDal;

        public ProductCategoryManager(IProductCategoryDal productCategoryDal, IProductDal productDal)
        {
            _productCategoryDal = productCategoryDal;
            _productDal = productDal;
        }

        public IDataResult<ProductCategoryDto> GetById(int Id)
        {
            ProductCategoryDto productCategoryDto = new ProductCategoryDto();
            var productCategories = _productCategoryDal.GetList().ToList();

            foreach (var productCategory in productCategories)
            {

                productCategoryDto.Id = productCategory.Id;
                productCategoryDto.Name = productCategory.Name;
                productCategoryDto.Description = productCategory.Description;
                productCategoryDto.CategoryType = productCategory.CategoryType;
                productCategoryDto.Products = _productDal.GetList(p => p.ProductCatalogId == productCategory.Id).ToList();
                productCategoryDto = productCategoryDto;
            };
            return new IResult<ProductCategoryDto>(productCategoryDto);
        }

        public IDataResult<List<ProductCategoryDto>> GetList()
        {
            List<ProductCategoryDto> productCategoryDtos = new List<ProductCategoryDto>();
            var productCategories = _productCategoryDal.GetList().ToList();

            foreach (var productCategory in productCategories)
            {
                ProductCategoryDto productCategoryDto = new ProductCategoryDto();
                productCategoryDto.Id = productCategory.Id;
                productCategoryDto.Name = productCategory.Name;
                productCategoryDto.Description = productCategory.Description;
                productCategoryDto.CategoryType = productCategory.CategoryType;
                productCategoryDto.Products = _productDal.GetList(p => p.ProductCatalogId == productCategory.Id).ToList();
                productCategoryDtos.Add(productCategoryDto);
            };
            return new IResult<List<ProductCategoryDto>>(productCategoryDtos);
        }
        public IDataResult<List<ProductCategoryDto>> GetListByName(string Name)
        {
            List<ProductCategoryDto> productCategoryDtos = new List<ProductCategoryDto>();

            var productCategories = _productCategoryDal
                .GetList(p => p.Name == Name).ToList();

            foreach (var productCategory in productCategories)
            {

                ProductCategoryDto productCategoryDto = new ProductCategoryDto();
                productCategoryDto.Id = productCategory.Id;
                productCategoryDto.Name = productCategory.Name;
                productCategoryDto.Description = productCategory.Description;
                productCategoryDto.CategoryType = productCategory.CategoryType;
                productCategoryDto.Products = _productDal.GetList(p => p.ProductCatalogId == productCategory.Id).ToList();
                productCategoryDtos.Add(productCategoryDto);
            };
            return new IResult<List<ProductCategoryDto>>(productCategoryDtos);
        }
        public IDataResult<List<ProductCategoryDto>> GetListByCategoryType(string CategoryType)
        {
            List<ProductCategoryDto> productCategoryDtos = new List<ProductCategoryDto>();
            var productCategories = _productCategoryDal
                .GetList(p => p.CategoryType == CategoryType).ToList();

            foreach (var productCategory in productCategories)
            {

                ProductCategoryDto productCategoryDto = new ProductCategoryDto();
                productCategoryDto.Id = productCategory.Id;
                productCategoryDto.Name = productCategory.Name;
                productCategoryDto.Description = productCategory.Description;
                productCategoryDto.CategoryType = productCategory.CategoryType;
                productCategoryDto.Products = _productDal.GetList(p => p.ProductCatalogId == productCategory.Id).ToList();
                productCategoryDtos.Add(productCategoryDto);
            };

            return new IResult<List<ProductCategoryDto>>(productCategoryDtos);
        }

        public IResult Update(ProductCategory ProductCategory)
        {
            _productCategoryDal.Update(ProductCategory);
            return new SuccessResult(Messages.ProductCategoryUpdated);
        }

        public IResult Add(ProductCategory ProductCategory)
        {
            _productCategoryDal.Add(ProductCategory);
            return new SuccessResult(Messages.ProductCategoryAdded);
        }

        public IResult Delete(int id)
        {
            var products = _productDal.GetList(p => p.ProductCatalogId == id);
            foreach (var product in products)
            {
                _productDal.Delete(product);
            }

            ProductCategory productCategory = _productCategoryDal.Get(p => p.Id == id);
            _productCategoryDal.Delete(productCategory);

            return new SuccessResult(Messages.ProductCategoryDeleted);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public IResult Add(Product product)
        {
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(int id)
        {
            Product product = _productDal.Get(p => p.Id == id);
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        public IDataResult<List<Product>> GetList()
        {
            return new IResult<List<Product>>(_productDal.GetList().ToList());
        }

        public IDataResult<Product> GetById(int Id)
        {
            return new IResult<Product>(_productDal.Get(p => p.Id == Id));
        }

        public IDataResult<List<Product>> GetListByCategoryId(int ProductCatalogId)
        {
            return new IResult<List<Product>>(_productDal
                .GetList(p => p.ProductCatalogId == ProductCatalogId).ToList());
        }

        public IDataResult<List<Product>> GetListByPriceRange(int price1, int price2)
        {
            return new IResult<List<Product>>(_productDal
                .GetList(p => (price1 <= p.Price && price2 >= p.Price)).ToList());
        }

        public IDataResult<List<Product>> GetListByProductName(string Name)
        {
            return new IResult<List<Product>>(_productDal
                .GetList(p => p.Name == Name).ToList());
        }

        public IDataResult<List<Product>> GetListByQuantityPerUnit(int QuantityPerUnit)
        {
            return new IResult<List<Product>>(_productDal
                .GetList(p => p.QuantityPerUnit == QuantityPerUnit).ToList());
        }

        public IDataResult<List<Product>> GetListByUnitsInStock(int UnitsInStock)
        {
            return new IResult<List<Product>>(_productDal
                .GetList(p => p.UnitsInStock == UnitsInStock).ToList());
        }

        public IDataResult<List<Product>> GetListByUnitsOnOrder(int UnitsOnOrder)
        {
            return new IResult<List<Product>>(_productDal
                .GetList(p => p.UnitsOnOrder == UnitsOnOrder).ToList());
        }
    }
}

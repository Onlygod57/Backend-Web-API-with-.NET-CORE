using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IProductCategoryService
    {
        IDataResult<List<ProductCategoryDto>>GetList();
        IDataResult<ProductCategoryDto> GetById(int Id);
        IDataResult<List<ProductCategoryDto>> GetListByName(string Name);
        IDataResult<List<ProductCategoryDto>> GetListByCategoryType(string CategoryType);
        IResult Add(ProductCategory ProductCategory);
        IResult Update(ProductCategory ProductCategory);
        IResult Delete(int id);
    }
}

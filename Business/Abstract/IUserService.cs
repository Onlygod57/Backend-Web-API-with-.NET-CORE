using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<User> GetById(int Id);
        List<OperationClaim> GetClaims(User user);
        User GetByEmail(string Email);
        IDataResult<List<User>> GetList();
        IResult Add(User user);
        IResult Update(User user);
    }
}

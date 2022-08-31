using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        public User GetByEmail(string Email)
        {
            return _userDal.Get(filter: u => u.Email == Email);
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }
        public IDataResult<User> GetById(int Id)
        {
            return new IResult<User>(_userDal.Get(p => p.Id == Id));
        }
        public IDataResult<List<User>> GetList()
        {
            return new IResult<List<User>>(_userDal.GetList().ToList());
        }
        public IResult Update(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.UserUpdated);
        }
    }
}

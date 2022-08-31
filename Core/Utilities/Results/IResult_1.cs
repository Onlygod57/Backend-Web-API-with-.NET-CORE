using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class IResult<T> : DataResult<T>
    {
        public IResult(T data, string message) : base(data, true)
        {
        }
        public IResult(T data) : base(data, true)
        {
        }
        public IResult(string message) : base(default, true, message)
        {

        }
    }

}

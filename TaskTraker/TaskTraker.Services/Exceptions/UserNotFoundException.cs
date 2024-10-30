using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskTraker.Services.Exceptions
{
    public class UserNotFoundException : ApplicationBaseException
    {
        public UserNotFoundException() : base("User not found", HttpStatusCode.NotFound)
        {

        }

    }
}

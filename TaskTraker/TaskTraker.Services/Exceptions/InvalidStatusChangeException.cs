using System.Net;

namespace TaskTraker.Services.Exceptions
{
    public class InvalidStatusChangeException : ApplicationBaseException
    {
        public InvalidStatusChangeException() : base("Invalid status change", HttpStatusCode.BadRequest)
        {

        }
    }
}

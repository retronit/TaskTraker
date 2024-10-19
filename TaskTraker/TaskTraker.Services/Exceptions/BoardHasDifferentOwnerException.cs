using System.Net;

namespace TaskTraker.Services.Exceptions
{
    public class BoardHasDifferentOwnerException : ApplicationBaseException
    {
        public BoardHasDifferentOwnerException() : base("Board has different owner", HttpStatusCode.Forbidden)
        {

        }
    }
}

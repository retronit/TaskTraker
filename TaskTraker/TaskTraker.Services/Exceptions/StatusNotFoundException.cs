using System.Net;

namespace TaskTraker.Services.Exceptions
{
    public class StatusNotFoundException : ApplicationBaseException
    {
        public StatusNotFoundException() : base("Status not found", HttpStatusCode.NotFound)
        {

        }
    }
}

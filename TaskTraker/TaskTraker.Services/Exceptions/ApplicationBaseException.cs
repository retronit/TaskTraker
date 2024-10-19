using System.Net;

namespace TaskTraker.Services.Exceptions
{
    public class ApplicationBaseException(string message, HttpStatusCode status) : Exception(message)
    {
        public HttpStatusCode Status { get; } = status;
    }
}

using System.Net;

namespace TaskTraker.Services.Exceptions
{
    public class TaskItemHasDifferentOwnerException : ApplicationBaseException
    {
        public TaskItemHasDifferentOwnerException() : base("Task item has different owner", HttpStatusCode.Forbidden)
        {

        }
    }
}

using System.Net;

namespace TaskTraker.Services.Exceptions
{
    public class TaskItemNotFoundException : ApplicationBaseException
    {
        public TaskItemNotFoundException() : base("Task item not found", HttpStatusCode.NotFound)
        {

        }
    }
}

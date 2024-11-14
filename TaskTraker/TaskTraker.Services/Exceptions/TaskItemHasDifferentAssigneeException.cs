using System.Net;

namespace TaskTraker.Services.Exceptions
{
    public class TaskItemHasDifferentAssigneeException : ApplicationBaseException
    {
        public TaskItemHasDifferentAssigneeException() : base("Task item has different assignee", HttpStatusCode.Forbidden)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskTraker.Services.Exceptions
{
    public class TaskItemNotInBoardException : ApplicationBaseException
    {
        public TaskItemNotInBoardException() : base("Task item not in the board", HttpStatusCode.NotFound)
        {

        }
    }
}

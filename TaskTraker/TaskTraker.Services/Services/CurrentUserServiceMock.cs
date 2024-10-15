using TaskTraker.Services.Interfaces;

namespace TaskTraker.Services.Services
{
    public class CurrentUserServiceMock : ICurrentUserServiceMock
    {
        public int UserId => 1;
    }
}

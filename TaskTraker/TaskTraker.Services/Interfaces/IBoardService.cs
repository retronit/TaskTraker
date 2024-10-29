using TaskTraker.Data.Models;
using TaskTraker.Services.Dtos;

namespace TaskTraker.Services.Interfaces
{
    public interface IBoardService
    {
        Task<IEnumerable<GetBoardDto>> GetAllAsync();

        Task<GetBoardDto> GetAsync(int id);

        Task<IEnumerable<User>> GetAllCollaboratorsAsync(int id);

        Task CreateAsync(CreateBoardDto board);

        Task UpdateNameAsync(UpdateBoardDto boardDto);

        Task DeleteAsync(int id);
    }
}

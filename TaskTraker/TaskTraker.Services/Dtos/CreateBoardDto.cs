using TaskTraker.Data.Models;
using TaskTraker.Services.Interfaces;

namespace TaskTraker.Services.Dtos
{
    public record CreateBoardDto(string? Name);

    public static class CreateBoardDtoExtensions
    {
        public static CreateBoardDto FromBoardToCreateDto(this Board board) => new
         (
             Name: board.Name
         );

        public static Board FromCreateDtoToBoard(this CreateBoardDto boardDto, ICurrentUserServiceMock userServiceMock)
        {
            return new Board
            {
                Name = boardDto.Name,
                OwnerId = userServiceMock.UserId
            };
        }
    }
}


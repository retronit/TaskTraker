using TaskTraker.Data.Models;

namespace TaskTraker.Services.Dtos
{
    public record UpdateBoardDto(int Id, string? Name);

    public static class UpdateBoardDtoExtensions
    {
        public static UpdateBoardDto FromBoardToUpdateDto(this Board board) => new
         (
             Id: board.Id,
             Name: board.Name
         );

        public static void FromUpdateDtoToBoard(this UpdateBoardDto boardDto, Board board)
        {
            board.Id = boardDto.Id;
            board.Name = boardDto.Name;
        }
    }
}
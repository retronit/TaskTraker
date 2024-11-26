using Microsoft.EntityFrameworkCore;
using TaskTraker.Data.Context;
using TaskTraker.Services.Dtos;
using TaskTraker.Services.Exceptions;
using TaskTraker.Services.Interfaces;

namespace TaskTraker.Services.Services
{
    public class BoardService(TaskTrakerDbContext context, ICurrentUserService currentUserService) : IBoardService
    {
        private readonly TaskTrakerDbContext _context = context;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<IEnumerable<GetBoardDto>> GetAllAsync()
        {
            var boards = await _context.Boards
                .Include(b => b.Collaborators)
                .Where(x => x.Collaborators.Any(user => user.Id == _currentUserService.UserId))
                .ToListAsync();

            var boardDtos = boards.Select(x => x.FromBoardToGetDto()).ToList();

            return boardDtos;
        }

        public async Task<GetBoardDto> GetAsync(int id)
        {
            var board = await _context.Boards
                .Include(b => b.Collaborators)
                .FirstOrDefaultAsync(b => b.Id == id) ?? throw new BoardNotFoundException();

            if (!board.Collaborators.Any(user => user.Id == _currentUserService.UserId))
            {
                throw new BoardHasDifferentOwnerException();
            }

            return board.FromBoardToGetDto();
        }

        public async Task CreateAsync(CreateBoardDto boardDto)
        {
            var board = boardDto.FromCreateDtoToBoard();

            var user = await _context.Users.FindAsync(_currentUserService.UserId) ?? throw new UserNotFoundException();

            board.Collaborators.Add(user);

            _context.Boards.Add(board);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var board = await _context.Boards
                .Include(b => b.Collaborators)
                .FirstOrDefaultAsync(b => b.Id == id) ?? throw new BoardNotFoundException();

            if (!board.Collaborators.Any(user => user.Id == _currentUserService.UserId))
            {
                throw new BoardHasDifferentOwnerException();
            }

            _context.Boards.Remove(board);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateNameAsync(UpdateBoardDto boardDto)
        {
            var existingBoard = await _context.Boards
                .Include(b => b.Collaborators)
                .FirstOrDefaultAsync(b => b.Id == boardDto.Id) ?? throw new BoardNotFoundException();

            if (!existingBoard.Collaborators.Any(user => user.Id == _currentUserService.UserId))
            {
                throw new BoardHasDifferentOwnerException();
            }

            boardDto.FromUpdateDtoToBoard(existingBoard);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetUserDto>> GetAllCollaboratorsAsync(int id)
        {
            var board = await _context.Boards
                .Include(b => b.Collaborators)
                .FirstOrDefaultAsync(b => b.Id == id) ?? throw new BoardNotFoundException();

            var collaborators = board.Collaborators.ToList();

            var collaboratorDtos = collaborators.Select(x => x.FromUserToGetDto()).ToList();

            return collaboratorDtos;
        }

        public async Task AddCollaboratorsAsync(int boardId, List<string> userIds)
        {
            var board = await _context.Boards
                .Include(b => b.Collaborators) 
                .FirstOrDefaultAsync(b => b.Id == boardId) ?? throw new BoardNotFoundException();

            var usersToAdd = await _context.Users
                .Where(user => userIds.Contains(user.Id))
                .ToListAsync();

            foreach (var user in usersToAdd)
            {
                if (!board.Collaborators.Contains(user))
                {
                    board.Collaborators.Add(user);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveCollaboratorsAsync(int boardId, List<string> userIds)
        {
            var board = await _context.Boards
                .Include(b => b.Collaborators) 
                .FirstOrDefaultAsync(b => b.Id == boardId) ?? throw new BoardNotFoundException();

            var usersToRemove = board.Collaborators
                .Where(user => userIds.Contains(user.Id))
                .ToList();

            foreach (var user in usersToRemove)
            {
                board.Collaborators.Remove(user);
            }

            await _context.SaveChangesAsync();
        }
    }
}

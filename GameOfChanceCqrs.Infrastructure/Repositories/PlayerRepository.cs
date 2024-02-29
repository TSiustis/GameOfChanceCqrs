using GameOfChanceCqrs.Infrastructure.Data;
using GameOfChangeCqrs.Domain.Entities;
using GameOfChangeCqrs.Domain.Interfaces;

namespace GameOfChanceCqrs.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationDbContext _context;

        public PlayerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Player?> GetPlayerByIdAsync(int playerId)
        {
            return await _context.Players.FindAsync(playerId);
        }

        public async Task UpdatePlayerBalanceAsync(Player player, int balanceChange)
        {
            player.AccountBalance += balanceChange;

            await _context.SaveChangesAsync();
        }
    }
}
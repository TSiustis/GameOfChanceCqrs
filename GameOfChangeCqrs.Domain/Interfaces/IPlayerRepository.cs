using GameOfChangeCqrs.Domain.Entities;

namespace GameOfChangeCqrs.Domain.Interfaces
{
    public interface IPlayerRepository
    {
        Task<Player?> GetPlayerByIdAsync(int playerId);
        Task UpdatePlayerBalanceAsync(Player player, int balanceChange);
    }
}

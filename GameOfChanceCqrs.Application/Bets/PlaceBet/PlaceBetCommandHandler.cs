using GameOfChanceCqrs.Application.Bets.ViewModels.Output;
using GameOfChanceCqrs.Application.Common.CustomExceptions;
using GameOfChangeCqrs.Domain.Constants;
using GameOfChangeCqrs.Domain.Interfaces;
using MediatR;

namespace GameOfChanceCqrs.Application.Bets.PlaceBet
{
    public class PlaceBetCommandHandler : IRequestHandler<PlaceBetCommand, BetOutputVm>
    {
        private readonly IPlayerRepository _playerRepository;
        private static readonly Random _random = new();

        public PlaceBetCommandHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<BetOutputVm> Handle(PlaceBetCommand request, CancellationToken cancellationToken)
        {
            // Validation would normally be done in a validator but skipping for simplicity
            ValidateGuessedNumberRange(request.NumberPredicted);

            var player = await _playerRepository.GetPlayerByIdAsync(request.PlayerId)
                ?? throw new KeyNotFoundException(BetConstants.PlayerNotFound);

            ValidateBetAmount(request.Points, player.AccountBalance);

            var generatedNumber = _random.Next(0, 10);
            bool won = generatedNumber == request.NumberPredicted;

            var pointsChange = CalculatePointsChange(request.Points, won);

            await _playerRepository.UpdatePlayerBalanceAsync(player, pointsChange);

            return new BetOutputVm
            {
                AccountBalance = player.AccountBalance,
                PointsChange = pointsChange,
                GeneratedNumber = generatedNumber,
                Status = won ? "won" : "lost"
            };
        }

        private static void ValidateGuessedNumberRange(int numberPredicted)
        {
            if (numberPredicted < 0 || numberPredicted > 9)
            {
                throw new AppException(BetConstants.InvalidNumberEntered);
            }
        }

        private static void ValidateBetAmount(int points, int accountBalance)
        {

            if (points > accountBalance)
            {
                throw new AppException(BetConstants.BetExceededAccountAmount);
            }

            if (points < 0)
            {
                throw new AppException(BetConstants.BetLowerThanZero);
            }
        }

        private static int CalculatePointsChange(int points, bool won) => won ? points * 9 : -points;
    }
}

namespace GameOfChanceCqrs.Application.Bets.PlaceBet
{
    using GameOfChanceCqrs.Application.Bets.ViewModels.Output;
    using MediatR;

    public class PlaceBetCommand : IRequest<BetOutputVm>
    {
        public int PlayerId { get; set; }
        public int Points { get; set; }
        public int NumberPredicted { get; set; }
    }
}

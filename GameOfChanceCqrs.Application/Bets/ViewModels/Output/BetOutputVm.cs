namespace GameOfChanceCqrs.Application.Bets.ViewModels.Output
{
    public class BetOutputVm
    {
        public int AccountBalance { get; set; }
        public int PointsChange { get; set; }
        public int GeneratedNumber { get; set; }
        public string? Status { get; set; }
    }
}

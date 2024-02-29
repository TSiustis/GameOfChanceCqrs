using GameOfChanceCqrs.Infrastructure.Data;
using GameOfChangeCqrs.Domain.Entities;

namespace GameOfChanceCqrs.Api
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Players.Any())
            {
                return;
            }

            var players = new Player[]
            {
                new(),
                new(),
            };

            foreach (Player p in players)
            {
                context.Players.Add(p);
            }

            context.SaveChanges();
        }
    }
}

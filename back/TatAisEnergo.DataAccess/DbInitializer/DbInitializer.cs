using TatAisEnergo.Core.Entities;

namespace TatAisEnergo.DataAccess.DbInitializer
{
    /// <summary>
    /// Initialize database for 1000 records
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// Adds records to <see cref="History"/> entity
        /// </summary>
        /// <param name="context"></param>
        public static void SeedTestData(AppDbContext context)
        {
            if (context.Histories.Any()) return;

            var random = new Random();

            var users = context.Users.ToList();
            var eventTypes = context.EventTypes.ToList();

            var histories = Enumerable.Range(1, 1000).Select(i => new History
            {
                Id = i,
                Text = $"Событие #{i}",
                Dt = DateTime.UtcNow.AddHours(-random.Next(10000)),
                UserId = users[random.Next(users.Count)].Id,
                User = users[random.Next(users.Count)],
                EventTypeId = eventTypes[random.Next(eventTypes.Count)].Id,
                EventType = eventTypes[random.Next(eventTypes.Count)]
            });

            context.Histories.AddRange(histories);
            context.SaveChanges();
        }
    }
}
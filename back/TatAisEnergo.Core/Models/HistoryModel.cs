namespace TatAisEnergo.Core.Models
{
    public class HistoryModel
    {
        public long Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public long EventType { get; set; }
    }
}

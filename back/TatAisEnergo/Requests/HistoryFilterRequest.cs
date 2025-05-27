namespace TatAisEnergo.WebApi.Requests
{
    public class HistoryFilterRequest
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        // Filter
        public string? Id { get; set; }

        public string? Text { get; set; }

        public string? Name { get; set; }

        public long? EventType { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        // Sorting
        public List<SortField>? Sort { get; set; }
    }

    public class SortField
    {
        public string Field { get; set; } = string.Empty;

        public string Order { get; set; } = string.Empty;
    }
}

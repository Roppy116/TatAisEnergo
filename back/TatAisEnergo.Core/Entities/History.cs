using System.ComponentModel.DataAnnotations;

namespace TatAisEnergo.Core.Entities
{
    public class History
    {
        /// <summary>
        /// History entity identifier
        /// </summary>
        [Key]
        public required long Id { get; set; }

        /// <summary>
        /// Additional text
        /// </summary>
        [MaxLength(100)]
        public string? Text { get; set; }

        /// <summary>
        /// Date and time of event
        /// </summary>
        public DateTime Dt { get; set; }

        /// <summary>
        /// User identifier
        /// </summary>
        public required string UserId { get; set; }

        /// <summary>
        /// Link to user entity
        /// </summary>
        public required User User { get; set; }

        /// <summary>
        /// Event type identifier
        /// </summary>
        public required long EventTypeId { get; set; }

        /// <summary>
        /// Link to event type entity
        /// </summary>
        public required EventType EventType { get; set; }
    }
}

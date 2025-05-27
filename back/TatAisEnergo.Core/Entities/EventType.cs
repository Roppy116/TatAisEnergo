using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TatAisEnergo.Core.Entities
{
    public class EventType
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public required long Id { get; set; }

        /// <summary>
        /// Name of event
        /// </summary>
        [MaxLength(100)]
        public required string Name { get; set; }
    }
}

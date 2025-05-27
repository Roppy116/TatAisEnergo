using System.ComponentModel.DataAnnotations;

namespace TatAisEnergo.Core.Entities
{
    public class User
    {
        /// <summary>
        /// User identifier
        /// </summary>
        [Key]
        public required string Id { get; set; }

        /// <summary>
        /// Fullname of user
        /// </summary>
        [MaxLength(100)]
        public required string FullName { get; set; }
    }
}

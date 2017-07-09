using System.ComponentModel.DataAnnotations;

namespace InsuranceTestApp.Models
{
    /// <summary>
    /// Class used to store customer data.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Internal ID for the customer.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Customer's name.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        /// <summary>
        /// Customer's mailing address.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string MailingAddress { get; set; }

        /// <summary>
        /// Customer's city.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string City { get; set; }

        /// <summary>
        /// Customer's state.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string State { get; set; }

        /// <summary>
        /// Customer's zip code.
        /// </summary>
        [Required]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"\d{5}-?(\d{4})?$")]
        public string ZipCode { get; set; }
    }
}

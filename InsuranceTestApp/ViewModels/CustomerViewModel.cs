using System.ComponentModel.DataAnnotations;
using InsuranceTestApp.Utilities;

namespace InsuranceTestApp.ViewModels
{
    /// <summary>
    /// Class used to customer data to display to the user.
    /// </summary>
    public class CustomerViewModel
    {
        /// <summary>
        /// Unique Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the primary insured for the policy.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(Helpers.DefaultMaximumStringLength, MinimumLength = 1, ErrorMessage = "Name must be fewer than 100 characters.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Mailing address of the primary insured.
        /// </summary>
        [Required(ErrorMessage = "Address is required")]
        [StringLength(Helpers.DefaultMaximumStringLength, MinimumLength = 1, ErrorMessage = "Address must be fewer than 100 characters.")]
        [Display(Name = "Mailing Address")]
        public string MailingAddress { get; set; }

        /// <summary>
        /// City of the primary insured.
        /// </summary>
        [Required(ErrorMessage = "City is required")]
        [StringLength(Helpers.DefaultMaximumStringLength, MinimumLength = 1, ErrorMessage = "City must be fewer than 100 characters.")]
        [Display(Name = "City")]
        public string City { get; set; }

        /// <summary>
        /// State of the primary insured.
        /// </summary>
        [Required(ErrorMessage = "State is required")]
        [StringLength(Helpers.DefaultMaximumStringLength, MinimumLength = 1, ErrorMessage = "State must be fewer than 100 characters.")]
        [Display(Name = "State")]
        public string State { get; set; }

        /// <summary>
        /// Zip code of the primary insured.
        /// </summary>
        [Required(ErrorMessage = "Zip Code is required")]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"\d{5}-?(\d{4})?$", ErrorMessage = "Zip Code must match XXXXX or XXXXX-XXXX format.")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
    }
}

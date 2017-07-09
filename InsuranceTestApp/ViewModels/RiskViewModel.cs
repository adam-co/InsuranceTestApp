using System.ComponentModel.DataAnnotations;
using InsuranceTestApp.Enums;
using InsuranceTestApp.Utilities;

namespace InsuranceTestApp.ViewModels
{
    /// <summary>
    /// Class used to store risk data to display to the user.
    /// </summary>
    public class RiskViewModel
    {
        /// <summary>
        /// Unique Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Type of risk construction.
        /// </summary>
        [Required(ErrorMessage = "Construction type is required")]
        [Display(Name = "Construction Type")]
        [Range(0, 3, ErrorMessage = "Please select a valid Construction Type value")]
        public RiskConstructionType ConstructionType { get; set; }

        /// <summary>
        /// Year the risk was built.
        /// </summary>
        [Required(ErrorMessage = "Year built is required")]
        [Display(Name = "Year Built")]
        [RegularExpression(@"\d{4}?$", ErrorMessage = "Year must match 'yyyy' format.")]
        public string YearBuilt { get; set; }

        /// <summary>
        /// Address of the risk.
        /// </summary>
        [Required(ErrorMessage = "Address is required")]
        [StringLength(Helpers.DefaultMaximumStringLength, MinimumLength = 1, ErrorMessage = "Address must be fewer than 100 characters.")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        /// <summary>
        /// City of the risk.
        /// </summary>
        [Required(ErrorMessage = "City is required")]
        [StringLength(Helpers.DefaultMaximumStringLength, MinimumLength = 1, ErrorMessage = "City must be fewer than 100 characters.")]
        [Display(Name = "City")]
        public string City { get; set; }

        /// <summary>
        /// State of the risk.
        /// </summary>
        [Required(ErrorMessage = "State is required")]
        [StringLength(Helpers.DefaultMaximumStringLength, MinimumLength = 1, ErrorMessage = "State must be fewer than 100 characters.")]
        [Display(Name = "State")]
        public string State { get; set; }

        /// <summary>
        /// Zip code of the risk.
        /// </summary>
        [Required(ErrorMessage = "Zip Code is required")]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"\d{5}-?(\d{4})?$", ErrorMessage = "Zip code must match 'XXXXX' or 'XXXXX-XXXX' format.")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
    }
}

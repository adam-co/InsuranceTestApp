using System.ComponentModel.DataAnnotations;
using InsuranceTestApp.Utilities;

namespace InsuranceTestApp.ViewModels
{
    /// <summary>
    /// Class used to store insurance policy data to display to the user.
    /// </summary>
    public class PolicyViewModel
    {
        /// <summary>
        /// Unique Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Number used to reference the policy.
        /// </summary>
        [Required(ErrorMessage = "Policy Number is required")]
        [StringLength(Helpers.DefaultMaximumStringLength, MinimumLength = 1, ErrorMessage = "Policy Number must be fewer than 100 characters.")]
        [Display(Name = "Policy Number")]
        public string PolicyNumber { get; set; }

        /// <summary>
        /// Effective start date for the policy.
        /// </summary>
        [Required(ErrorMessage = "Effective Date is required")]
        [Display(Name = "Effective Date")]
        public string EffectiveDate { get; set; }

        /// <summary>
        /// Expiration date for the policy.
        /// </summary>
        [Required(ErrorMessage = "Expiration Date is required")]
        [Display(Name = "Expiration Date")]
        public string ExpirationDate { get; set; }

        /// <summary>
        /// Primary insured customer.
        /// </summary>
        [Display(Name = "Primary Insured")]
        public CustomerViewModel PrimaryInsured { get; set; }

        /// <summary>
        /// Insured risk.
        /// </summary>
        [Display(Name = "Risk")]
        public RiskViewModel Risk { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceTestApp.Models
{
    /// <summary>
    /// Class used to store insurance policy data.
    /// </summary>
    public class Policy
    {
        /// <summary>
        /// Internal ID for the insurance policy.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Number used to reference the policy.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string PolicyNumber { get; set; }

        /// <summary>
        /// Effective start date for the policy.
        /// </summary>
        [Required]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// Expiration date for the policy.
        /// </summary>
        [Required]
        public DateTime ExpirationDate { get; set; }
        
        /// <summary>
        /// Primary insured customer unique ID (foreign key).
        /// </summary>
        public int? PrimaryInsuredId { get; set; }

        /// <summary>
        /// Primary insured customer for the policy.
        /// </summary>
        [ForeignKey("PrimaryInsuredId")]
        public Customer PrimaryInsured { get; set; }

        /// <summary>
        /// Risk unique ID (foreign key).
        /// </summary>
        public int? RiskId { get; set; }

        /// <summary>
        /// Risk insured by the policy.
        /// </summary>
        [ForeignKey("RiskId")]
        public Risk Risk { get; set; }
    }
}

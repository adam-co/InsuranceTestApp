using System;
using System.ComponentModel.DataAnnotations;
using InsuranceTestApp.Enums;

namespace InsuranceTestApp.Models
{
    /// <summary>
    /// Class used to store risk data.
    /// </summary>
    public class Risk
    {
        /// <summary>
        /// Internal ID for the insurance policy.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Type of risk construction.
        /// </summary>
        [Required]
        [Range(0, 3, ErrorMessage = "Please select a valid Construction Type value")]
        public RiskConstructionType ConstructionType { get; set; }

        /// <summary>
        /// Year the risk was built.
        /// </summary>
        [Required]
        [RegularExpression(@"\d{4}?$", ErrorMessage = "Year must match 'yyyy' format.")]
        public string YearBuilt { get; set; }

        /// <summary>
        /// Address of the risk.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Address { get; set; }

        /// <summary>
        /// City of the risk.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string City { get; set; }

        /// <summary>
        /// State of the risk.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string State { get; set; }

        /// <summary>
        /// Zip code of the risk.
        /// </summary>
        [Required]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"\d{5}-?(\d{4})?$", ErrorMessage = "Zip code must match 'XXXXX' or 'XXXXX-XXXX' format.")]
        public string ZipCode { get; set; }
    }
}

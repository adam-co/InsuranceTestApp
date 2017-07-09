using System.ComponentModel.DataAnnotations;

namespace InsuranceTestApp.Enums
{
    /// <summary>
    /// Enumeration used to store different risk construction types.
    /// </summary>
    public enum RiskConstructionType
    {
        /// <summary>
        /// A site built home
        /// </summary>
        [Display(Name = "Site Built Home")]
        SiteBuiltHome,

        /// <summary>
        /// A modular home.
        /// </summary>
        [Display(Name = "Modular Home")]
        ModularHome,

        /// <summary>
        /// A single-wide manufactured home.
        /// </summary>
        [Display(Name = "Single Wide Manufactured Home")]
        SingleWideManufacturedHome,

        /// <summary>
        /// A double-wide manufactured home.
        /// </summary>
        [Display(Name = "Double Wide Manufactured Home")]
        DoubleWideManufacturedHome
        
    }
}

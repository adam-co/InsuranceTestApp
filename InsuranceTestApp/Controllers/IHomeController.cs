using System.Web.Mvc;
using InsuranceTestApp.ViewModels;

namespace InsuranceTestApp.Controllers
{
    /// <summary>
    /// Interface for policy controllers.
    /// </summary>
    public interface IHomeController
    {
        /// <summary>
        /// Default index action.
        /// </summary>
        /// <returns>Action result.</returns>
        ActionResult Index();

        /// <summary>
        /// Creates a partial view for a creating a new policy.
        /// </summary>
        /// <returns>Partial view for creating a new policy.</returns>
        PartialViewResult CreatePolicyPartialView();

        /// <summary>
        /// Retrieves a list of insurance policies in a JTable friendly display format.
        /// </summary>
        /// <param name="jtStartIndex">Start index for filtering results.</param>
        /// <param name="jtPageSize">Page size used for filtering results.</param>
        /// <param name="jtSorting">Sorting string used for filtering results.</param>
        /// <returns></returns>
        JsonResult PolicyList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null);

        /// <summary>
        /// Creates a new insurance policy.
        /// </summary>
        /// <param name="policyViewModel">Policy to create.</param>
        /// <returns>Action result.</returns>
        JsonResult CreatePolicy(PolicyViewModel policyViewModel);

        /// <summary>
        /// Returns a JTable display list of risk construction types.
        /// </summary>
        /// <returns>JTable display list of risk construction types.</returns>
        JsonResult GetRiskConstructionTypes();
    }
}

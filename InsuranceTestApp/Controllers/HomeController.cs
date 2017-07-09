using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using InsuranceTestApp.Enums;
using InsuranceTestApp.Models;
using InsuranceTestApp.Repositories;
using InsuranceTestApp.Utilities;
using InsuranceTestApp.ViewModels;

namespace InsuranceTestApp.Controllers
{
    /// <summary>
    /// Controller class used to display insurance policies.
    /// </summary>
    public class HomeController : Controller, IHomeController
    {
        /// <summary>
        /// Unit of work used to access repositories.
        /// </summary>
        private IUnitOfWork unitOfWork = null;

        /// <summary>
        /// Stores a list of risk construction type display names.
        /// </summary>
        private static readonly List<Tuple<string, RiskConstructionType>> riskConstructionTypeOptionsList =
        (from RiskConstructionType riskConstructionType in Enum.GetValues(typeof(RiskConstructionType))
            let displayName = riskConstructionType.GetType()
                .GetMember(riskConstructionType.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .GetName()
            select new Tuple<string, RiskConstructionType>(displayName, riskConstructionType)).ToList();

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks>Use a IoC container to help with dependency injection.</remarks>
        public HomeController()
        {
            unitOfWork = new GenericUnitOfWork();
        }

        /// <summary>
        /// Constructor that accepts a unit of work dependency.
        /// </summary>
        /// <param name="unitOfWork">Unit of work.</param>
        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc cref="IHomeController.Index"/>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <inheritdoc cref="IHomeController.CreatePolicyPartialView"/>
        [HttpGet]
        public PartialViewResult CreatePolicyPartialView()
        {
            return PartialView("_CreatePolicyPartial", new PolicyViewModel());
        }

        /// <inheritdoc cref="IHomeController.PolicyList"/>
        [HttpPost]
        public JsonResult PolicyList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var policyViewModels = unitOfWork.Repository<Policy>().GetAll()
                    .Include(p => p.PrimaryInsured)
                    .Include(p => p.Risk)
                    .ToList()
                    .Select(policyModel => policyModel.ConvertToViewModel()).ToList();

                //Return result to jTable
                return Json(new { Result = "OK", Records = policyViewModels, TotalRecordCount = policyViewModels.Count() });
            }
            catch (Exception ex)
            {
                // TODO: Log full exception message to server.
                return CreateJsonErrorResult(ex);
            }
        }

        /// <inheritdoc cref="IHomeController.CreatePolicy"/>
        [HttpPost]
        public JsonResult CreatePolicy(PolicyViewModel policyViewModel)
        {
            try
            {
                // Ensure view model is valid before continuing.
                var jsonResult = ValidateModelState();

                if (jsonResult.Data.ToString().Length == 0)
                {
                    var policyModel = policyViewModel.ConvertToModel();
                    unitOfWork.Repository<Policy>().Add(policyModel);
                    unitOfWork.SaveChanges();

                    jsonResult = Json(new { Result = "OK", Record = policyViewModel });
                }

                return jsonResult;
            }
            catch (Exception ex)
            {
                return CreateJsonErrorResult(ex);
            }
        }

        /// <inheritdoc cref="IHomeController.GetRiskConstructionTypes"/>
        [HttpPost]
        public JsonResult GetRiskConstructionTypes()
        {
            try
            {
                var riskConstructionTypeOptions =
                    riskConstructionTypeOptionsList.Select(
                        tuple => new {DisplayText = tuple.Item1, Value = (int) tuple.Item2}).ToList();

                return Json(new { Result = "OK", Options = riskConstructionTypeOptions });
            }
            catch (Exception ex)
            {
                return CreateJsonErrorResult(ex);
            }
        }

        /// <summary>
        /// Validates the current model state, and returns an error JSON result if invalid.
        /// </summary>
        /// <returns>Error JSON result if invalid. Empty JSON result if valid.</returns>
        private JsonResult ValidateModelState()
        {
            var jsonResult = Json("");

            if (!ModelState.IsValid)
            {
                string errorMessages = string.Join("; ", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage));
                jsonResult = Json(new {Result = "ERROR", Message = $"Invalid form entry: {errorMessages}"});
            }

            return jsonResult;
        }

        /// <summary>
        /// Creates a JTable friendly JSON error result from the specified exception.
        /// </summary>
        /// <remarks>
        /// TODO: Log full exception message to server.
        /// </remarks>
        /// <param name="exception">The exception to include.</param>
        /// <returns>JTable friendly JSON error result from the specified exception.</returns>
        private JsonResult CreateJsonErrorResult(Exception exception)
        {
            var jsonErrorResult = Json(new { Result = "ERROR", Message = exception.Message });

            var entityValidationException = exception as DbEntityValidationException;
            if (entityValidationException != null)
            {
                string errorMessages = string.Join("; ", entityValidationException.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage));
                jsonErrorResult = Json(new { Result = "ERROR", Message = $"Invalid data received: {errorMessages}" });
            }

            return jsonErrorResult;
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using InsuranceTestApp.Utilities;

namespace InsuranceTestApp.UnitTests.Utilities
{
    /// <summary>
    /// Contains utility methods for unit testing.
    /// </summary>
    public static class TestUtilities
    {
        /// <summary>
        /// Contains a long, invalid string used for testing validation limits.
        /// </summary>
        public static string LongInvalidTestString = new string('a', Helpers.DefaultMaximumStringLength + 1);

        /// <summary>
        /// Validates the specified model.
        /// </summary>
        /// <remarks>
        /// Adapted from: https://stackoverflow.com/questions/2167811/unit-testing-asp-net-dataannotations-validation
        /// </remarks>
        /// <param name="model">Model to validate.</param>
        /// <returns>Result of the validation.</returns>
        public static IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        /// <summary>
        /// Retrieves a property using reflection. Used for checking JSON data.
        /// </summary>
        /// <remarks>
        /// Adapted from: https://stackoverflow.com/questions/4989471/how-to-unit-test-an-action-method-which-returns-jsonresult
        /// </remarks>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetReflectedProperty(this object obj, string propertyName)
        {
            PropertyInfo property = obj.GetType().GetProperty(propertyName);

            if (property == null)
            {
                return null;
            }

            return property.GetValue(obj, null);
        }
    }
}

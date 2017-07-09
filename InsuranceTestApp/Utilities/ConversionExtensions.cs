using System;
using System.Globalization;
using InsuranceTestApp.Models;
using InsuranceTestApp.ViewModels;

namespace InsuranceTestApp.Utilities
{
    /// <summary>
    /// Class stores utility conversion extensions.
    /// </summary>
    public static class ConversionExtensions
    {
        /// <summary>
        /// Date time format used during conversion.
        /// </summary>
        private const string DateTimeFormat = @"yyyy-MM-dd";

        /// <summary>
        /// Converts a <see cref="PolicyViewModel"/> to a <see cref="Policy"/> model.
        /// </summary>
        /// <param name="policyViewModel">View model to convert.</param>
        /// <returns>Converted model.</returns>
        public static Policy ConvertToModel(this PolicyViewModel policyViewModel)
        {
            var policyModel = new Policy()
            {
                Id = policyViewModel.Id,
                PolicyNumber = policyViewModel.PolicyNumber,
                EffectiveDate = DateTime.Parse(policyViewModel.EffectiveDate, CultureInfo.InvariantCulture),
                ExpirationDate = DateTime.Parse(policyViewModel.ExpirationDate, CultureInfo.InvariantCulture),
                PrimaryInsuredId = policyViewModel.PrimaryInsured.Id,
                PrimaryInsured = new Customer
                {
                    Id = policyViewModel.PrimaryInsured.Id,
                    City = policyViewModel.PrimaryInsured.City,
                    MailingAddress = policyViewModel.PrimaryInsured.MailingAddress,
                    Name = policyViewModel.PrimaryInsured.Name,
                    State = policyViewModel.PrimaryInsured.State,
                    ZipCode = policyViewModel.Risk.ZipCode,
                },
                RiskId = policyViewModel.Risk.Id,
                Risk = new Risk
                {
                    Id = policyViewModel.Risk.Id,
                    Address = policyViewModel.Risk.Address,
                    City = policyViewModel.Risk.City,
                    ConstructionType = policyViewModel.Risk.ConstructionType,
                    State = policyViewModel.Risk.State,
                    YearBuilt = policyViewModel.Risk.YearBuilt,
                    ZipCode = policyViewModel.Risk.ZipCode,
                }
            };

            return policyModel;
        }

        /// <summary>
        /// Converts a <see cref="Policy"/> model to a <see cref="PolicyViewModel"/>.
        /// </summary>
        /// <param name="policyModel">Model to convert.</param>
        /// <returns>Converted view model.</returns>
        public static PolicyViewModel ConvertToViewModel(this Policy policyModel)
        {
            var policyViewModel = new PolicyViewModel()
            {
                Id = policyModel.Id,
                PolicyNumber = policyModel.PolicyNumber,
                EffectiveDate = policyModel.EffectiveDate.ToString(DateTimeFormat),
                ExpirationDate = policyModel.ExpirationDate.ToString(DateTimeFormat),
                PrimaryInsured = new CustomerViewModel()
                {
                    Id = policyModel.PrimaryInsured.Id,
                    Name = policyModel.PrimaryInsured.Name,
                    City = policyModel.PrimaryInsured.City,
                    MailingAddress = policyModel.PrimaryInsured.MailingAddress,
                    State = policyModel.PrimaryInsured.State,
                    ZipCode = policyModel.PrimaryInsured.ZipCode,
                },
                Risk = new RiskViewModel()
                {
                    Id = policyModel.Risk.Id,
                    City = policyModel.Risk.City,
                    ConstructionType = policyModel.Risk.ConstructionType,
                    State = policyModel.Risk.State,
                    ZipCode = policyModel.Risk.ZipCode,
                    Address = policyModel.Risk.Address,
                    YearBuilt = policyModel.Risk.YearBuilt
                }
            };

            return policyViewModel;
        }
    }
}

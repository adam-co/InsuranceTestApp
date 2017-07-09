﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace InsuranceTestApp.Utilities
{
    /// <summary>
    /// Collection of helper methods and properties.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Static constructor used to populate lists once.
        /// </summary>
        static Helpers()
        {
            YearList = GetYearList();
            StateList = GetStateList();
        }

        /// <summary>
        /// Zip code regular expression string.
        /// </summary>
        public static string ZipCodeRegEx = @"\d{5}-?(\d{4})?$";

        /// <summary>
        /// Default max length for string values.
        /// </summary>
        public const int DefaultMaximumStringLength = 100;

        /// <summary>
        /// Contains a list of years.
        /// </summary>
        public static readonly List<SelectListItem> YearList;
        
        /// <summary>
        /// Retrieves a list of years, used to populate the public year list.
        /// </summary>
        /// <returns>A list of years.</returns>
        private static List<SelectListItem> GetYearList()
        {
            var yearList = new List<SelectListItem>();

            // TODO: Determine valid year range.
            for (var i = DateTime.Now.Year; i >= 1700; i--)
            {
                yearList.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });
            }

            return yearList;
        }

        /// <summary>
        /// Contains a list of states.
        /// </summary>
        public static readonly List<SelectListItem> StateList;

        /// <summary>
        /// Retrieves a list of states, used to populate the state list.
        /// </summary>
        /// <remarks>
        /// Adapted from: https://stackoverflow.com/questions/18539149/how-to-create-select-list-for-country-and-states-province-in-mvc
        /// </remarks>
        private static List<SelectListItem> GetStateList()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem() {Text = "Alabama", Value = "AL"},
                new SelectListItem() {Text = "Alaska", Value = "AK"},
                new SelectListItem() {Text = "Arizona", Value = "AZ"},
                new SelectListItem() {Text = "Arkansas", Value = "AR"},
                new SelectListItem() {Text = "California", Value = "CA"},
                new SelectListItem() {Text = "Colorado", Value = "CO"},
                new SelectListItem() {Text = "Connecticut", Value = "CT"},
                new SelectListItem() {Text = "District of Columbia", Value = "DC"},
                new SelectListItem() {Text = "Delaware", Value = "DE"},
                new SelectListItem() {Text = "Florida", Value = "FL"},
                new SelectListItem() {Text = "Georgia", Value = "GA"},
                new SelectListItem() {Text = "Hawaii", Value = "HI"},
                new SelectListItem() {Text = "Idaho", Value = "ID"},
                new SelectListItem() {Text = "Illinois", Value = "IL"},
                new SelectListItem() {Text = "Indiana", Value = "IN"},
                new SelectListItem() {Text = "Iowa", Value = "IA"},
                new SelectListItem() {Text = "Kansas", Value = "KS"},
                new SelectListItem() {Text = "Kentucky", Value = "KY"},
                new SelectListItem() {Text = "Louisiana", Value = "LA"},
                new SelectListItem() {Text = "Maine", Value = "ME"},
                new SelectListItem() {Text = "Maryland", Value = "MD"},
                new SelectListItem() {Text = "Massachusetts", Value = "MA"},
                new SelectListItem() {Text = "Michigan", Value = "MI"},
                new SelectListItem() {Text = "Minnesota", Value = "MN"},
                new SelectListItem() {Text = "Mississippi", Value = "MS"},
                new SelectListItem() {Text = "Missouri", Value = "MO"},
                new SelectListItem() {Text = "Montana", Value = "MT"},
                new SelectListItem() {Text = "Nebraska", Value = "NE"},
                new SelectListItem() {Text = "Nevada", Value = "NV"},
                new SelectListItem() {Text = "New Hampshire", Value = "NH"},
                new SelectListItem() {Text = "New Jersey", Value = "NJ"},
                new SelectListItem() {Text = "New Mexico", Value = "NM"},
                new SelectListItem() {Text = "New York", Value = "NY"},
                new SelectListItem() {Text = "North Carolina", Value = "NC"},
                new SelectListItem() {Text = "North Dakota", Value = "ND"},
                new SelectListItem() {Text = "Ohio", Value = "OH"},
                new SelectListItem() {Text = "Oklahoma", Value = "OK"},
                new SelectListItem() {Text = "Oregon", Value = "OR"},
                new SelectListItem() {Text = "Pennsylvania", Value = "PA"},
                new SelectListItem() {Text = "Rhode Island", Value = "RI"},
                new SelectListItem() {Text = "South Carolina", Value = "SC"},
                new SelectListItem() {Text = "South Dakota", Value = "SD"},
                new SelectListItem() {Text = "Tennessee", Value = "TN"},
                new SelectListItem() {Text = "Texas", Value = "TX"},
                new SelectListItem() {Text = "Utah", Value = "UT"},
                new SelectListItem() {Text = "Vermont", Value = "VT"},
                new SelectListItem() {Text = "Virginia", Value = "VA"},
                new SelectListItem() {Text = "Washington", Value = "WA"},
                new SelectListItem() {Text = "West Virginia", Value = "WV"},
                new SelectListItem() {Text = "Wisconsin", Value = "WI"},
                new SelectListItem() {Text = "Wyoming", Value = "WY"}
            };
        }
    }
}

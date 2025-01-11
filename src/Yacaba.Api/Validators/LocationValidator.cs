using System.Text.RegularExpressions;
using FluentValidation;
using Yacaba.Domain.Models;

namespace Yacaba.Api.Validators {
    public class LocationValidator : AbstractValidator<GpsLocation> {

        public LocationValidator() {
            RuleFor(p => p.Latitude).NotEmpty().Matches(GpsLocationRegex.Regex());
            RuleFor(p => p.Longitude).NotEmpty().Matches(GpsLocationRegex.Regex());
        }
    }

    internal static partial class GpsLocationRegex {
        [GeneratedRegex("^(-?\\d+(\\.\\d+)?)$", RegexOptions.IgnoreCase)]
        internal static partial Regex Regex();
    }
}

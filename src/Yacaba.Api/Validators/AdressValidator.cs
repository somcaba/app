using FluentValidation;
using Yacaba.Domain.Models;

namespace Yacaba.Api.Validators {
    public class AdressValidator : AbstractValidator<Address> {
        public AdressValidator() {
            RuleFor(p => p.Line1).NotEmpty();
            RuleFor(p => p.PostalCode).MaximumLength(5).NotEmpty();
            RuleFor(p => p.Locality).NotEmpty();
            RuleFor(p => p.CountryCode).MaximumLength(2).NotEmpty();
        }
    }
}

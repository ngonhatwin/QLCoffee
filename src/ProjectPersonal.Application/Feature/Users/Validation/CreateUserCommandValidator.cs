using FluentValidation;
using ProjectPersonal.Application.Feature.Users.Commands.Create;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Feature.Users.Validation
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            //RuleFor(c => c.FullName).NotEmpty().WithMessage("Name must not be empty.");
            //RuleFor(p => p.PhoneNumber)
            //      .NotEmpty()
            //      .NotNull().WithMessage("Phone Number is required.")
            //      .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
            //      .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
            //      .Matches(new Regex(@"^\d{10,20}$")).WithMessage("PhoneNumber not valid");
            //RuleFor(p => p.Address)
            //    .NotEmpty()
            //    .NotNull().WithMessage("Address is required");
            RuleFor(s => s.Email).NotEmpty().WithMessage("Email address is required")
              .EmailAddress().WithMessage("A valid email is required");
            RuleFor(p => p.Username).NotEmpty().NotNull().WithMessage("Username is required.");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Your password cannot be empty")
                   .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                   .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                   .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                   .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                   .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
        }
    }
}

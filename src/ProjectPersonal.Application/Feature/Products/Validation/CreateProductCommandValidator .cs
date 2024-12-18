using FluentValidation;
using ProjectPersonal.Application.Feature.Products.Commands.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Feature.Products.Validator
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Product name must not be empty.");
            RuleFor(c => c.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }
}

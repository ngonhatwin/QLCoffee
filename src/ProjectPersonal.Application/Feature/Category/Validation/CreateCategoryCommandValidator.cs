using FluentValidation;
using ProjectPersonal.Application.Feature.Category.Commands.Create;
using ProjectPersonal.Application.Feature.Products.Commands.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Feature.Category.Validation
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Category name must not be empty.");
            RuleFor(c => c.Description).NotEmpty().WithMessage("Category Description name must not be empty.");
        }
    }
}

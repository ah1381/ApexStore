using FluentValidation;
using ApexStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexStore.Domain.Validator
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(o => o.Name)
                .NotEmpty().WithMessage("وارد کردن نام الزامیست.")
                .MaximumLength(30).WithMessage("نام نباید بیش از 30 کاراکتر داشته باشد.");

        }
    }
}

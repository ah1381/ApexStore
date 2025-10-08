using FluentValidation;
using ApexStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexStore.Domain.Validator
{
    public class ProductFeaturesValidator : AbstractValidator<ProductFeatures>
    {
        public ProductFeaturesValidator()
        {
            RuleFor(o => o.DisplayName)
                .NotEmpty().WithMessage("وارد کردن اسم ویژگی الزامیست.");

            RuleFor(o => o.Value)
                .NotEmpty().WithMessage("وارد کردن مقدار ویژگی الزامیست.");
        }
    }
}

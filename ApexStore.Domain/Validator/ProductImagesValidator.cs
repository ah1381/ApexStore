using FluentValidation;
using ApexStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexStore.Domain.Validator
{
    public class ProductImagesValidator : AbstractValidator<ProductImages>
    {
        public ProductImagesValidator()
        {
            RuleFor(o => o.Src)
                .NotEmpty().WithMessage("انتخاب فایل الزامیست.");
        }
    }
}

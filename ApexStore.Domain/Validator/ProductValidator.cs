using FluentValidation;
using ApexStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexStore.Domain.Validator
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(o => o.Name)
                .NotEmpty().WithMessage("وارد کردن نام الزامیست.")
                .MaximumLength(30).WithMessage("نام نباید بیش از 30 کاراکتر داشته باشد.");

            RuleFor(o => o.Brand)
                .NotEmpty().WithMessage("وارد کردن نام برند الزامیست.")
                .MaximumLength(30).WithMessage("نام برند نباید بیش از 30 کاراکتر داشته باشد.");

            RuleFor(o => o.Description)
                .NotEmpty().WithMessage("وارد کردن توضیحات الزامیست.");

            RuleFor(o => o.Price)
                .NotEmpty().WithMessage("قیمت نباید خالی باشد")
                .GreaterThan(0).WithMessage("قیمت میبایست بیشتر از صفر باشد.");

            RuleFor(o => o.Inventory)
                .NotEmpty().WithMessage("موجودی نباید خالی باشد");

            RuleFor(o => o.CategoryId)
                .NotEmpty().WithMessage("دسته بندی نباید خالی باشد.");

            RuleForEach(o => o.ProductFeatures)
                .SetValidator(new ProductFeaturesValidator());

            RuleForEach(o => o.ProductImages)
                .SetValidator(new ProductImagesValidator());

        }
    }
}

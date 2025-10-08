using FluentValidation;
using ApexStore.Domain.Entities.HomePages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexStore.Domain.Validator
{
    public class SliderValidator : AbstractValidator<Slider>
    {
        public SliderValidator()
        {
            RuleFor(o => o.Src)
                .NotEmpty().WithMessage("انتخاب مسیر عکس الزامیست.");

            RuleFor(o => o.link)
                .NotEmpty().WithMessage("وارد کردن لینک مورد نظر الزامیست.")
                .Must(o => o.Contains("/")).WithMessage("لینک وارد شده صحیح نمی باشد.");
                
        }
    }
}

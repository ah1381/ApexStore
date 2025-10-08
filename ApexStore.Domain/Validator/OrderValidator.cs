using FluentValidation;
using ApexStore.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexStore.Domain.Validator
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {

            RuleFor(o => o.Address)
                .NotEmpty().WithMessage("وارد کردن آدرس الزامیست")
                .MaximumLength(255).WithMessage("آدرس نباید بیش از 255 کاراکتر باشد.");

        }
    }
}

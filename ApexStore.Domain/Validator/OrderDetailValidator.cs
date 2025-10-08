using FluentValidation;
using ApexStore.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexStore.Domain.Validator
{
    public class OrderDetailValidator : AbstractValidator<OrderDetail>
    {
        public OrderDetailValidator()
        {
            RuleFor(o => o.Price)
                .NotEmpty().WithMessage("قیمت نباید خالی باشد")
                .GreaterThan(0).WithMessage("قیمت میبایست بیشتر از صفر باشد.");

            RuleFor(o => o.Count)
                .NotEmpty().WithMessage("تعداد نباید خالی باشد");

        }
    }
}

using FluentValidation;
using ApexStore.Domain.Entities.Finances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexStore.Domain.Validator
{
    public class RequestPayValidator : AbstractValidator<RequestPay>
    {
        public RequestPayValidator()
        {
            RuleFor(o => o.Amount)
                .NotEmpty().WithMessage("سبد خرید شما خالی ایست.");

        }
    }
}

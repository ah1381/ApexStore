using FluentValidation;
using ApexStore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexStore.Domain.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(o => o.FullName)
                .NotEmpty().WithMessage("وارد کردن نام الزامیست.")
                .MaximumLength(30).WithMessage("نام نباید بیش از 30 کاراکتر داشته باشد.");

            RuleFor(o => o.Email)
                .NotEmpty().WithMessage("وارد کردن ایمیل الزامیست.")
                .EmailAddress().WithMessage("ایمیل خود را به درستی وارد کنید.");

            RuleFor(o => o.Password)
                .NotEmpty().WithMessage("وارد کردن رمز عبور الزامیست.");
        }
    }
}

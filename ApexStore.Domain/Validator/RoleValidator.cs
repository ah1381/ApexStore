using FluentValidation;
using ApexStore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexStore.Domain.Validator
{
    public class RoleValidator : AbstractValidator<Role>
    {
        public RoleValidator()
        {
            RuleFor(o => o.Name)
                .NotEmpty().WithMessage("وارد کردن نام الزامیست.")
                .Length(30).WithMessage("نام نباید بیش از 30 کاراکتر داشته باشد.");

        }
    }
}

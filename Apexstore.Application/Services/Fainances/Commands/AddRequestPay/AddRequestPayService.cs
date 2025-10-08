using ApexStore.Application.Interface.Contexts;
using ApexStore.Common.Dto;
using ApexStore.Domain.Entities.Finances;
using ApexStore.Domain.Validator;

namespace ApexStore.Application.Services.Fainances.Commands.AddRequestPay
{
    public class AddRequestPayService : IAddRequestPayService
    {
        private readonly IMainDbContext _context;
        public AddRequestPayService(IMainDbContext context)
        {
            _context = context;
        }
        public ResultDto<ResultRequestPayDto> Execute(int Amount, long UserId)
        {
            var user = _context.Users.Find(UserId);
            RequestPay requestPay = new RequestPay()
            {
                Amount = Amount,
                Guid = Guid.NewGuid(),
                IsPay = false,
                User = user,
            };

            var validator = new RequestPayValidator();
            var resultValid = validator.Validate(requestPay);

            if (resultValid.IsValid)
                _context.RequestPays.Add(requestPay);
                _context.SaveChanges();

            return new ResultDto<ResultRequestPayDto>()
            {
                Data = new ResultRequestPayDto
                {
                    guid = requestPay.Guid,
                    Amount=requestPay.Amount,
                    Email=user.Email,
                    RequestPayId=requestPay.Id,
                },
                IsSuccess = true,
            };
        }
    }
}

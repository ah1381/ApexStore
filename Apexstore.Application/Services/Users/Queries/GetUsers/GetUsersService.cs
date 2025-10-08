using ApexStore.Application.Interface.Contexts;
using ApexStore.Common;

namespace ApexStore.Application.Services.Users.Queries.GetUsers
{
    public class GetUsersService : IGetUsersService
    {

        private readonly IMainDbContext _context;

        public GetUsersService(IMainDbContext context)
        {
            _context = context;
        }

        public ReslutGetUserDto Execute(RequestGetUserDto request)
        {
            var users = _context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(request.SearchKey))
            {
                users = users.Where(p => p.FullName.Contains(request.SearchKey) && p.Email.Contains(request.SearchKey));
            }
            int rowsCount = 0;
            var userList = users.Topaged(request.page, 20, out rowsCount).Select(p => new GetUsersDto
            { 
                Email = p.Email,
                FullName = p.FullName,
                Id = p.Id,
                IsActive = p.IsActive,
            }).ToList();

            return new ReslutGetUserDto
            {
                Rows = rowsCount,
                users = userList,
            };
        }
    }
}

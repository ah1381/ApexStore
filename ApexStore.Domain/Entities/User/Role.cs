using ApexStore.Domain.Entities.Commons;

namespace ApexStore.Domain.Entities.User
{
    public class Role : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserInRole> UserInRoles { get; set; }
    }
}

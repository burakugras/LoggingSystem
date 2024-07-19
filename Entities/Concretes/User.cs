using Core.Entities;

namespace Entities.Concretes
{
    public class User : Entity<int>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public List<Activity> Activities { get; set; }
    }
}

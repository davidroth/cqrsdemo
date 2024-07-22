namespace CqrsDemo.Core.Domain
{
    public class User : Entity, IAggregateRoot
    {
        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
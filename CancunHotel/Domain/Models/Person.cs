namespace CancunHotel.Api.Domain.Models
{
    public class Person
    {
        public string Name { get; }
        public string Email { get; }

        public Person(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}

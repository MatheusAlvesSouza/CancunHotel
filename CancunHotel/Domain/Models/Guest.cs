using System.Text.Json.Serialization;

namespace CancunHotel.Api.Domain.Models
{
    public class Guest : Person
    {
        public bool CheckIn { get; }

        [JsonConstructor]
        public Guest(string name, string email, bool checkIn) : base(name, email)
        {
            CheckIn = checkIn;
        }
    }
}

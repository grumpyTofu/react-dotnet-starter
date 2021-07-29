using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApi.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public List<Todo> Todos { get; } = new List<Todo>();
    }
}
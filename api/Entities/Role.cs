using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApi.Entities
{
    public enum Permissions
    {
        General,
        Admin
    }
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Permissions Permissions { get; set; }
    }

}
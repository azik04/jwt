using System.Text.Json.Serialization;

namespace jwt.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    [JsonIgnore] public string Password { get; set; }
}

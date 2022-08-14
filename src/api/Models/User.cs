using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.Models;

public class User
{
  public int Id { get; set; }

  [Required] 
  public string Name { get; set; }

  [Required] [EmailAddress] 
  public string Email { get; set; }

  [JsonIgnore] 
  public string Password { get; set; }
}

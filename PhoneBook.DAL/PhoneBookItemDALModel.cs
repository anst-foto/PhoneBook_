using System.Text.Json.Serialization;

namespace PhoneBook.DAL;

public class PhoneBookItemDALModel
{
    public int Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Phone { get; set; }

    [JsonIgnore] public string FullName => $"{LastName} {FirstName}";
}
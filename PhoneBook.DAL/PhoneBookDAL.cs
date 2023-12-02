using System.Text.Json;

namespace PhoneBook.DAL;

public class PhoneBookDAL : ICrud<PhoneBookItemDALModel>
{
    private List<PhoneBookItemDALModel> _phoneBook;
    private readonly string _path;

    public PhoneBookDAL(string path = "phone_book.json")
    {
        _path = path;
        var json = File.ReadAllText(_path);
        _phoneBook = JsonSerializer.Deserialize<List<PhoneBookItemDALModel>>(json);
    }

    private static void WriteToFile(IEnumerable<PhoneBookItemDALModel> phoneBook, string path)
    {
        var json = JsonSerializer.Serialize(phoneBook);
        File.WriteAllText(path, json);
    }

    public void Create(PhoneBookItemDALModel dalModel)
    {
        _phoneBook.Add(dalModel);
        
        WriteToFile(_phoneBook, _path);
    }

    public void Update(PhoneBookItemDALModel dalModel)
    {
        var item = _phoneBook.FirstOrDefault(i => i.Id == dalModel.Id);
        
        if (item is null) return;
        
        item.LastName = dalModel.LastName;
        item.FirstName = dalModel.FirstName;
        item.Phone = dalModel.Phone;
        
        WriteToFile(_phoneBook, _path);
    }

    public void Delete(PhoneBookItemDALModel dalModel)
    {
        var item = _phoneBook.FirstOrDefault(i => i.Id == dalModel.Id);
        
        if (item is null) return;

        _phoneBook.Remove(item);
        
        WriteToFile(_phoneBook, _path);
    }

    public IEnumerable<PhoneBookItemDALModel> GetAll()
    {
        return _phoneBook;
    }
}
using PhoneBook.DAL;

namespace PhoneBook.BL;

public class PhoneBookBL : ICrud<PhoneBookItemDALModel>
{
    private readonly ICrud<PhoneBookItemDALModel> _crud;

    public PhoneBookBL(ICrud<PhoneBookItemDALModel> crud)
    {
        _crud = crud;
    }

    public void Create(PhoneBookItemDALModel dalModel)
    {
        _crud.Create(dalModel);
    }

    public void Update(PhoneBookItemDALModel dalModel)
    {
        _crud.Update(dalModel);
    }

    public void Delete(PhoneBookItemDALModel dalModel)
    {
        _crud.Delete(dalModel);
    }

    public IEnumerable<PhoneBookItemDALModel> GetAll()
    {
        return _crud.GetAll();
    }
}
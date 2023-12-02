namespace PhoneBook.DAL;

public interface ICrud<T>
{
    public void Create(T model);
    public void Update(T model);
    public void Delete(T model);
    public IEnumerable<T> GetAll();
}
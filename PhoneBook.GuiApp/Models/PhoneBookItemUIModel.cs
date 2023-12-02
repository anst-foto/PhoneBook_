using ReactiveUI;

namespace PhoneBook.GuiApp.Models;

public class PhoneBookItemUIModel : ReactiveObject
{
    public static int LastId;
    
    private int _id;
    public int Id
    {
        get => _id;
        set
        {
            this.RaiseAndSetIfChanged(ref _id, value);
            if (Id > LastId)
            {
                LastId = Id;
            }
        }
    }
    
    private string _name;
    public string Name
    {
        get => _name; 
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    private string _phone;
    public string Phone
    {
        get => _phone;
        set => this.RaiseAndSetIfChanged(ref _phone, value);
    }
}
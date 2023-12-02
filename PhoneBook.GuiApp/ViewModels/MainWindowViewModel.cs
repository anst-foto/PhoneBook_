using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using PhoneBook.BL;
using PhoneBook.DAL;
using PhoneBook.GuiApp.Mappers;
using PhoneBook.GuiApp.Models;
using ReactiveUI;

namespace PhoneBook.GuiApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    #region ObservableProperties

    public ObservableCollection<PhoneBookItemUIModel> Collection { get; set; }
    
    private PhoneBookItemUIModel? _selectedItem;
    public PhoneBookItemUIModel? SelectedItem
    {
        get => _selectedItem;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItem, value);
            
            Id = SelectedItem?.Id;
            Name = SelectedItem?.Name;
            Phone = SelectedItem?.Phone;
        }
    }

    private int? _id;
    public int? Id
    {
        get => _id; 
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }
    
    private string? _name;
    public string? Name
    {
        get => _name; 
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }
    
    private string? _phone;
    public string? Phone
    {
        get => _phone; 
        set => this.RaiseAndSetIfChanged(ref _phone, value);
    }

    #endregion

    #region ReactiveCommand

    public ReactiveCommand<Unit, Unit> ClearCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> NewCommand { get; }

    #endregion

    public MainWindowViewModel()
    {
        var phoneBook = new PhoneBookBL(new PhoneBookDAL());
        
        Collection = new ObservableCollection<PhoneBookItemUIModel>(phoneBook.GetAll().Select(Mapper.MapToUiModel));

        var isSelectedItem = this.WhenAnyValue(
            x => x.SelectedItem,
            x => x.SelectedItem,
            (item, _) => item is not null);
        var isNameAndPhone = this.WhenAnyValue(
            x => x.Name,
            x => x.Phone,
            (name, phone) => !string.IsNullOrWhiteSpace(name) && 
                             !string.IsNullOrWhiteSpace(phone));
        var isNameOrPhone = this.WhenAnyValue(
            x => x.Name,
            x => x.Phone,
            (name, phone) => !string.IsNullOrWhiteSpace(name) || 
                             !string.IsNullOrWhiteSpace(phone));
        ClearCommand = ReactiveCommand.Create(
            execute: () =>
            {
                SelectedItem = null;
                Clear();
            },
            canExecute: isNameOrPhone);
        DeleteCommand = ReactiveCommand.Create(
            execute: () =>
            {
                phoneBook.Delete(Mapper.MapToDalModel(SelectedItem));
                Collection.Remove(SelectedItem);
                Clear();
            },
            canExecute: isSelectedItem);
        SaveCommand = ReactiveCommand.Create(
            execute: () =>
            {
                SelectedItem.Name = Name;
                SelectedItem.Phone = Phone;
                phoneBook.Update(Mapper.MapToDalModel(SelectedItem));
            },
            canExecute: isSelectedItem);
        NewCommand = ReactiveCommand.Create(
            execute: () =>
            {
                var item = new PhoneBookItemUIModel
                {
                    Id = PhoneBookItemUIModel.LastId + 1,
                    Name = Name,
                    Phone = Phone
                };
                Collection.Add(item);
                phoneBook.Create(Mapper.MapToDalModel(item));
                
                Clear();
            },
        canExecute: isNameAndPhone);
    }

    #region LocalFunctions

    private void Clear()
    {
        Id = null;
        Name = null;
        Phone = null;
    }

    #endregion
}
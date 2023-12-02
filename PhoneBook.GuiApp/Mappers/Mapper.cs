using System.Collections.Generic;
using System.Linq;
using PhoneBook.DAL;
using PhoneBook.GuiApp.Models;

namespace PhoneBook.GuiApp.Mappers;

public static class Mapper
{
    public static PhoneBookItemUIModel MapToUiModel(PhoneBookItemDALModel dalModel)
    {
        return new PhoneBookItemUIModel()
        {
            Id = dalModel.Id,
            Name = dalModel.FullName,
            Phone = dalModel.Phone
        };
    }

    public static PhoneBookItemDALModel MapToDalModel(PhoneBookItemUIModel model)
    {
        var names = model.Name.Split(' ');
        return new PhoneBookItemDALModel()
        {
            Id = model.Id,
            LastName = names[0],
            FirstName = names[1],
            Phone = model.Phone
        };
    }
}
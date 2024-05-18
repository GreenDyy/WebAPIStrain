using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;

namespace WebAPIStrain.Services
{
    public interface IAuthorNewspaperRepository
    {
        List<AuthorNewspaperVM> GetAll();
        AuthorNewspaperVM GetById(int id);
        AuthorNewspaperVM Create(AuthorNewspaperModel inputAuthorNewspaper);
        bool Update(int id, AuthorNewspaperModel inputAuthorNewspaper);
        bool Delete(int id);
    }
}

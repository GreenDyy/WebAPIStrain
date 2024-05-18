using Microsoft.EntityFrameworkCore;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIStrain.Services
{
    public class AuthorNewspaperRepository : IAuthorNewspaperRepository
    {
        private readonly IrtContext dbContext;

        public AuthorNewspaperRepository(IrtContext context)
        {
            dbContext = context;
        }

        public AuthorNewspaperVM Create(AuthorNewspaperModel authorNewspaper)
        {
            var newAuthorNewspaper = new AuthorNewspaper
            {
                IdNewspaper = authorNewspaper.IdNewspaper,
                IdEmployee = authorNewspaper.IdEmployee,
                PostDate = authorNewspaper.PostDate,
                RoleOfAuthor = authorNewspaper.RoleOfAuthor
            };
            dbContext.Add(newAuthorNewspaper);
            dbContext.SaveChanges();
            return new AuthorNewspaperVM
            {
                IdNewspaper = newAuthorNewspaper.IdNewspaper,
                IdEmployee = newAuthorNewspaper.IdEmployee,
                PostDate = newAuthorNewspaper.PostDate,
                RoleOfAuthor = newAuthorNewspaper.RoleOfAuthor
            };
        }

        public bool Delete(int id)
        {
            var authorNewspaper = dbContext.AuthorNewspapers.FirstOrDefault(p => p.IdNewspaper == id);
            if (authorNewspaper != null)
            {
                dbContext.Remove(authorNewspaper);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<AuthorNewspaperVM> GetAll()
        {
            var authorNewspapers = dbContext.AuthorNewspapers.Select(p => new AuthorNewspaperVM
            {
                IdNewspaper = p.IdNewspaper,
                IdEmployee = p.IdEmployee,
                PostDate = p.PostDate,
                RoleOfAuthor = p.RoleOfAuthor
            }).ToList();
            return authorNewspapers;
        }

        public AuthorNewspaperVM GetById(int id)
        {
            var authorNewspaper = dbContext.AuthorNewspapers.FirstOrDefault(p => p.IdNewspaper == id);
            if (authorNewspaper != null)
            {
                return new AuthorNewspaperVM
                {
                    IdNewspaper = authorNewspaper.IdNewspaper,
                    IdEmployee = authorNewspaper.IdEmployee,
                    PostDate = authorNewspaper.PostDate,
                    RoleOfAuthor = authorNewspaper.RoleOfAuthor
                };
            }
            return null;
        }

        public bool Update(int id, AuthorNewspaperModel authorNewspaper)
        {
            var _authorNewspaper = dbContext.AuthorNewspapers.FirstOrDefault(p => p.IdNewspaper == id);
            if (_authorNewspaper != null)
            {
                _authorNewspaper.IdNewspaper = authorNewspaper.IdNewspaper;
                _authorNewspaper.IdEmployee = authorNewspaper.IdEmployee;
                _authorNewspaper.PostDate = authorNewspaper.PostDate;
                _authorNewspaper.RoleOfAuthor = authorNewspaper.RoleOfAuthor;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
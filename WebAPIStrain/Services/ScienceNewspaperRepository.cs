using Microsoft.EntityFrameworkCore;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIStrain.Services
{
    public class ScienceNewspaperRepository : IScienceNewspaperRepository
    {
        private readonly IrtContext dbContext;

        public ScienceNewspaperRepository(IrtContext context)
        {
            dbContext = context;
        }

        public ScienceNewspaperVM Create(ScienceNewspaperModel scienceNewspaper)
        {
            var newScienceNewspaper = new ScienceNewspaper
            {
                Title = scienceNewspaper.Title,
                Content = scienceNewspaper.Content,
                PostDate = scienceNewspaper.PostDate,
                Image = scienceNewspaper.Image,
                IdEmployee = scienceNewspaper.IdEmployee,
                Content2 = scienceNewspaper.Content2,
            };
            dbContext.Add(newScienceNewspaper);
            dbContext.SaveChanges();
            return new ScienceNewspaperVM
            {
                IdNewspaper = newScienceNewspaper.IdNewspaper,
                Title = newScienceNewspaper.Title,
                Content = newScienceNewspaper.Content,
                PostDate = newScienceNewspaper.PostDate,
                Image = newScienceNewspaper.Image,
                IdEmployee = newScienceNewspaper?.IdEmployee,
                Content2 = newScienceNewspaper?.Content2,
            };
        }

        public bool Delete(int id)
        {
            var scienceNewspaper = dbContext.ScienceNewspapers.FirstOrDefault(p => p.IdNewspaper == id);
            if (scienceNewspaper != null)
            {
                dbContext.Remove(scienceNewspaper);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<ScienceNewspaperVM> GetAll()
        {
            var scienceNewspapers = dbContext.ScienceNewspapers.Select(p => new ScienceNewspaperVM
            {
                IdNewspaper = p.IdNewspaper,
                Title = p.Title,
                Content = p.Content,
                PostDate = p.PostDate,
                Image = p.Image,
                IdEmployee = p.IdEmployee,
                Content2 = p.Content2,
            }).ToList();
            return scienceNewspapers;
        }

        public ScienceNewspaperVM GetById(int id)
        {
            var scienceNewspaper = dbContext.ScienceNewspapers.FirstOrDefault(p => p.IdNewspaper == id);
            if (scienceNewspaper != null)
            {
                return new ScienceNewspaperVM
                {
                    IdNewspaper = scienceNewspaper.IdNewspaper,
                    Title = scienceNewspaper.Title,
                    Content = scienceNewspaper.Content,
                    PostDate = scienceNewspaper.PostDate,
                    Image = scienceNewspaper.Image,
                    IdEmployee = scienceNewspaper.IdEmployee,
                    Content2 = scienceNewspaper.Content2,
                };
            }
            return null;
        }

        public bool Update(int id, ScienceNewspaperModel scienceNewspaper)
        {
            var _scienceNewspaper = dbContext.ScienceNewspapers.FirstOrDefault(p => p.IdNewspaper == id);
            if (_scienceNewspaper != null)
            {
                _scienceNewspaper.Title = scienceNewspaper.Title;
                _scienceNewspaper.Content = scienceNewspaper.Content;
                _scienceNewspaper.PostDate = scienceNewspaper.PostDate;
                _scienceNewspaper.Image = scienceNewspaper.Image;
                _scienceNewspaper.IdEmployee = scienceNewspaper.IdEmployee;
                _scienceNewspaper.Content2 = scienceNewspaper.Content2;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public List<ScienceNewspaperVM> GetRandom()
        {
            var scienceNewspapers = dbContext.ScienceNewspapers
                .OrderBy(x => Guid.NewGuid().ToString())
                .Take(5)
                .Select(p => new ScienceNewspaperVM
                {
                    IdNewspaper = p.IdNewspaper,
                    Title = p.Title,
                    Content = p.Content,
                    PostDate = p.PostDate,
                    Image = p.Image,
                    IdEmployee = p.IdEmployee,
                    Content2 = p.Content2,
                }).ToList();
            return scienceNewspapers;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIStrain.Services
{
    public class ContentWorkRepository : IContentWorkRepository
    {
        private readonly IrtContext dbContext;

        public ContentWorkRepository(IrtContext context)
        {
            dbContext = context;
        }

        public ContentWorkVM Create(ContentWorkModel contentWork)
        {
            var newContentWork = new ContentWork
            {
                IdProjectContent = contentWork.IdProjectContent,
                IdEmployee = contentWork.IdEmployee,
                NameContent = contentWork.NameContent,
                Results = contentWork.Results,
                StartDate = contentWork.StartDate,
                EndDate = contentWork.EndDate,
                ContractNo = contentWork.ContractNo,
                Status = contentWork.Status,
                Priority = contentWork.Priority
            };
            dbContext.Add(newContentWork);
            dbContext.SaveChanges();
            return new ContentWorkVM
            {
                IdContentWork = newContentWork.IdContentWork,
                IdProjectContent = newContentWork.IdProjectContent,
                IdEmployee = newContentWork.IdEmployee,
                NameContent = newContentWork.NameContent,
                Results = newContentWork.Results,
                StartDate = newContentWork.StartDate,
                EndDate = newContentWork.EndDate,
                ContractNo = newContentWork.ContractNo,
                Status = newContentWork.Status,
                Priority = newContentWork.Priority
            };
        }

        public bool Delete(int id)
        {
            var contentWork = dbContext.ContentWorks.FirstOrDefault(p => p.IdContentWork == id);
            if (contentWork != null)
            {
                dbContext.Remove(contentWork);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<ContentWorkVM> GetAll()
        {
            var contentWorks = dbContext.ContentWorks.Select(p => new ContentWorkVM
            {
                IdContentWork = p.IdContentWork,
                IdProjectContent = p.IdProjectContent,
                IdEmployee = p.IdEmployee,
                NameContent = p.NameContent,
                Results = p.Results,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                ContractNo = p.ContractNo,
                Status = p.Status,
                Priority = p.Priority
            }).ToList();
            return contentWorks;
        }

        public ContentWorkVM GetById(int id)
        {
            var contentWork = dbContext.ContentWorks.FirstOrDefault(p => p.IdContentWork == id);
            if (contentWork != null)
            {
                return new ContentWorkVM
                {
                    IdContentWork = contentWork.IdContentWork,
                    IdProjectContent = contentWork.IdProjectContent,
                    IdEmployee = contentWork.IdEmployee,
                    NameContent = contentWork.NameContent,
                    Results = contentWork.Results,
                    StartDate = contentWork.StartDate,
                    EndDate = contentWork.EndDate,
                    ContractNo = contentWork.ContractNo,
                    Status = contentWork.Status,
                    Priority = contentWork.Priority
                };
            }
            return null;
        }

        public bool Update(int id, ContentWorkModel contentWork)
        {
            var _contentWork = dbContext.ContentWorks.FirstOrDefault(p => p.IdContentWork == id);
            if (_contentWork != null)
            {
                _contentWork.IdProjectContent = contentWork.IdProjectContent;
                _contentWork.IdEmployee = contentWork.IdEmployee;
                _contentWork.NameContent = contentWork.NameContent;
                _contentWork.Results = contentWork.Results;
                _contentWork.StartDate = contentWork.StartDate;
                _contentWork.EndDate = contentWork.EndDate;
                _contentWork.ContractNo = contentWork.ContractNo;
                _contentWork.Status = contentWork.Status;
                _contentWork.Priority = contentWork.Priority;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
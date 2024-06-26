﻿using Microsoft.EntityFrameworkCore;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;

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
                Priority = contentWork.Priority,
                EndDateActual = contentWork.EndDateActual,
                Notificattion = contentWork.Notificattion,
                Title = contentWork.Title,
                SubTitle = contentWork.SubTitle,
                FileSaved = contentWork.FileSaved,
                FileName = contentWork.FileName,
                Histories = contentWork.Histories,
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
                Priority = newContentWork.Priority,
                EndDateActual = newContentWork.EndDateActual,
                Notificattion = newContentWork.Notificattion,
                Title = newContentWork.Title,
                SubTitle = newContentWork.SubTitle,
                FileSaved = newContentWork.FileSaved,
                FileName = newContentWork.FileName,
                Histories = newContentWork.Histories,
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
                Priority = p.Priority,
                EndDateActual = p.EndDateActual,
                Notificattion = p.Notificattion,
                Title = p.Title,
                SubTitle = p.SubTitle,
                FileSaved = p.FileSaved,
                FileName = p.FileName,
                Histories = p.Histories,
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
                    Priority = contentWork.Priority,
                    EndDateActual = contentWork.EndDateActual,
                    Notificattion = contentWork.Notificattion,
                    Title = contentWork.Title,
                    SubTitle = contentWork.SubTitle,
                    FileSaved = contentWork.FileSaved,
                    FileName = contentWork.FileName,
                    Histories = contentWork.Histories,
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
                _contentWork.EndDateActual = contentWork.EndDateActual;
                _contentWork.Notificattion = contentWork.Notificattion;
                _contentWork.Title = contentWork.Title;
                _contentWork.SubTitle = contentWork.SubTitle;
                _contentWork.Histories = contentWork.Histories;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateStatusProjectContent(int idProjectContent, string status)
        {
            var _strain = dbContext.ProjectContents.FirstOrDefault(s => s.IdProjectContent == idProjectContent);
            if (_strain != null)
            {
                _strain.Status = status;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateFileSaveAndName(int idContentWork, byte[] fileSave, string fileName)
        {
            var _strain = dbContext.ContentWorks.FirstOrDefault(s => s.IdContentWork == idContentWork);
            if (_strain != null)
            {
                _strain.FileSaved = fileSave;
                _strain.FileName = fileName;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateStatusContentWork(int idContentWork, string result, string endDateActual)
        {
            var _strain = dbContext.ContentWorks.FirstOrDefault(s => s.IdContentWork == idContentWork);
            if (_strain != null)
            {
                _strain.Results = result;
                _strain.EndDateActual = DateOnly.Parse(endDateActual);
                _strain.Status = "Đã hoàn thành";
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateNotificationNull(int idContentWork)
        {
            var _strain = dbContext.ContentWorks.FirstOrDefault(s => s.IdContentWork == idContentWork);
            if (_strain != null)
            {
                _strain.Notificattion = "Nhân viên đã xem yêu cầu";
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<ContentWorkVM> GetAllByIdEmployee(string idEmployee)
        {
            var contentWorks = dbContext.ContentWorks.Where(p => p.IdEmployee == idEmployee).Select(p => new ContentWorkVM
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
                Priority = p.Priority,
                EndDateActual = p.EndDateActual,
                Notificattion = p.Notificattion,
                Title = p.Title,
                SubTitle = p.SubTitle,
                FileSaved = p.FileSaved,
                FileName = p.FileName,
                Histories = p.Histories,
            }).ToList();
            return contentWorks;
        }
        public List<ContentWorkVM> GetAllByIdProjectContent(int idProjectContent)
        {
            var contentWorks = dbContext.ContentWorks.Where(p => p.IdProjectContent == idProjectContent).Select(p => new ContentWorkVM
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
                Priority = p.Priority,
                EndDateActual = p.EndDateActual,
                Notificattion = p.Notificattion,
                Title = p.Title,
                SubTitle = p.SubTitle,
                FileSaved = p.FileSaved,
                FileName = p.FileName,
                Histories = p.Histories,
            }).ToList();
            return contentWorks;
        }
    }
}
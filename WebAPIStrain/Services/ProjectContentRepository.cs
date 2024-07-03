using Microsoft.EntityFrameworkCore;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIStrain.Services
{
    public class ProjectContentRepository : IProjectContentRepository
    {
        private readonly IrtContext dbContext;

        public ProjectContentRepository(IrtContext context)
        {
            dbContext = context;
        }

        public ProjectContentVM Create(ProjectContentModel projectContent)
        {
            var newProjectContent = new ProjectContent
            {
                IdProject = projectContent.IdProject,
                NameContent = projectContent.NameContent,
                Results = projectContent.Results,
                StartDate = projectContent.StartDate,
                EndDate = projectContent.EndDate,
                ContractNo = projectContent.ContractNo,
                Status = projectContent.Status,
                Priority = projectContent.Priority,
                Title = projectContent.Title,
            };
            dbContext.Add(newProjectContent);
            dbContext.SaveChanges();
            return new ProjectContentVM
            {
                IdProjectContent = newProjectContent.IdProjectContent,
                IdProject = newProjectContent.IdProject,
                NameContent = newProjectContent.NameContent,
                Results = newProjectContent.Results,
                StartDate = newProjectContent.StartDate,
                EndDate = newProjectContent.EndDate,
                ContractNo = newProjectContent.ContractNo,
                Status = newProjectContent.Status,
                Priority = newProjectContent.Priority,
                Title = newProjectContent.Title,
            };
        }

        public bool Delete(int id)
        {
            var projectContent = dbContext.ProjectContents.FirstOrDefault(p => p.IdProjectContent == id);
            if (projectContent != null)
            {
                dbContext.Remove(projectContent);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<ProjectContentVM> GetAll()
        {
            var projectContents = dbContext.ProjectContents.Select(p => new ProjectContentVM
            {
                IdProjectContent = p.IdProjectContent,
                IdProject = p.IdProject,
                NameContent = p.NameContent,
                Results = p.Results,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                ContractNo = p.ContractNo,
                Status = p.Status,
                Priority = p.Priority,
                Title = p.Title,
            }).ToList();
            return projectContents;
        }

        public ProjectContentVM GetById(int id)
        {
            var projectContent = dbContext.ProjectContents.FirstOrDefault(p => p.IdProjectContent == id);
            if (projectContent != null)
            {
                return new ProjectContentVM
                {
                    IdProjectContent = projectContent.IdProjectContent,
                    IdProject = projectContent.IdProject,
                    NameContent = projectContent.NameContent,
                    Results = projectContent.Results,
                    StartDate = projectContent.StartDate,
                    EndDate = projectContent.EndDate,
                    ContractNo = projectContent.ContractNo,
                    Status = projectContent.Status,
                    Priority = projectContent.Priority,
                    Title = projectContent.Title,
                };
            }
            return null;
        }

        public bool Update(int id, ProjectContentModel projectContent)
        {
            var _projectContent = dbContext.ProjectContents.FirstOrDefault(p => p.IdProjectContent == id);
            if (_projectContent != null)
            {
                _projectContent.IdProject = projectContent.IdProject;
                _projectContent.NameContent = projectContent.NameContent;
                _projectContent.Results = projectContent.Results;
                _projectContent.StartDate = projectContent.StartDate;
                _projectContent.EndDate = projectContent.EndDate;
                _projectContent.ContractNo = projectContent.ContractNo;
                _projectContent.Status = projectContent.Status;
                _projectContent.Priority = projectContent.Priority;
                _projectContent.Title = projectContent.Title;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateStatusProject(string idProject, string status)
        {
            var _query = dbContext.Projects.FirstOrDefault(s => s.IdProject == idProject);
            if (_query != null)
            {
                _query.Status = status;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateStatusProjectContent(int idProjectContent, string status)
        {
            var _query = dbContext.ProjectContents.FirstOrDefault(s => s.IdProjectContent == idProjectContent);
            if (_query != null)
            {
                _query.Status = status;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<ProjectContentVM> GetAllByIdProject(string idProject)
        {
            var projectContents = dbContext.ProjectContents.Where(p=>p.IdProject == idProject).Select(p => new ProjectContentVM
            {
                IdProjectContent = p.IdProjectContent,
                IdProject = p.IdProject,
                NameContent = p.NameContent,
                Results = p.Results,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                ContractNo = p.ContractNo,
                Status = p.Status,
                Priority = p.Priority,
                Title = p.Title,
            }).ToList();
            return projectContents;
        }
    }
}
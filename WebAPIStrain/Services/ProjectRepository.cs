using Microsoft.EntityFrameworkCore;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIStrain.Services
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IrtContext dbContext;

        public ProjectRepository(IrtContext context)
        {
            dbContext = context;
        }

        public ProjectVM Create(ProjectModel project)
        {
            var newProject = new Project
            {
                IdProject = GenerateProjectId(),
                IdEmployee = project.IdEmployee,
                IdPartner = project.IdPartner,
                ProjectName = project.ProjectName,
                Results = project.Results,
                StartDateProject = project.StartDateProject,
                EndDateProject = project.EndDateProject,
                ContractNo = project.ContractNo,
                Description = project.Description,
                FileProject = project.FileProject,
                Status = project.Status,
                FileName = project.FileName
            };
            dbContext.Add(newProject);
            dbContext.SaveChanges();
            return new ProjectVM
            {
                IdProject = newProject.IdProject,
                IdEmployee = newProject.IdEmployee,
                IdPartner = newProject.IdPartner,
                ProjectName = newProject.ProjectName,
                Results = newProject.Results,
                StartDateProject = newProject.StartDateProject,
                EndDateProject = newProject.EndDateProject,
                ContractNo = newProject.ContractNo,
                Description = newProject.Description,
                FileProject = newProject.FileProject,
                Status = newProject.Status,
                FileName = newProject.FileName
            };
        }

        public bool Delete(string id)
        {
            var project = dbContext.Projects.FirstOrDefault(p => p.IdProject == id);
            if (project != null)
            {
                dbContext.Remove(project);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<ProjectVM> GetAll()
        {
            var projects = dbContext.Projects.Select(p => new ProjectVM
            {
                IdProject = p.IdProject,
                IdEmployee = p.IdEmployee,
                IdPartner = p.IdPartner,
                ProjectName = p.ProjectName,
                Results = p.Results,
                StartDateProject = p.StartDateProject,
                EndDateProject= p.EndDateProject,
                ContractNo = p.ContractNo,
                Description = p.Description,
                FileProject = p.FileProject,
                FileName = p.FileName,
                Status = p.Status
            }).ToList();
            return projects;
        }

        public ProjectVM GetById(string id)
        {
            var project = dbContext.Projects.FirstOrDefault(p => p.IdProject == id);
            if (project != null)
            {
                return new ProjectVM
                {
                    IdProject = project.IdProject,
                    IdEmployee = project.IdEmployee,
                    IdPartner = project.IdPartner,
                    ProjectName = project.ProjectName,
                    Results = project.Results,
                    StartDateProject = project.StartDateProject,
                    EndDateProject = project.EndDateProject,
                    ContractNo = project.ContractNo,
                    Description = project.Description,
                    FileProject = project.FileProject,
                    FileName = project.FileName,
                    Status = project.Status
                };
            }
            return null;
        }

        public bool Update(string id, ProjectModel project)
        {
            var _project = dbContext.Projects.FirstOrDefault(p => p.IdProject == id);
            if (_project != null)
            {
                _project.IdEmployee = project.IdEmployee;
                _project.IdPartner = project.IdPartner;
                _project.ProjectName = project.ProjectName;
                _project.Results = project.Results;
                _project.StartDateProject = project.StartDateProject;
                _project.EndDateProject = project.EndDateProject;
                _project.ContractNo = project.ContractNo;
                _project.Description = project.Description;
                _project.FileProject = project.FileProject;
                _project.FileName = project.FileName;
                _project.Status = project.Status;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        private string GenerateProjectId()
        {
            // Implement logic to generate a new project ID (e.g., DA0001 -> DA9999)
            var lastProject = dbContext.Projects
                .OrderByDescending(p => p.IdProject)
                .FirstOrDefault();

            if (lastProject == null)
            {
                return "DA0001";
            }

            var lastIdNumber = int.Parse(lastProject.IdProject.Substring(2));
            return $"DA{lastIdNumber + 1:D4}";
        }
    }
}
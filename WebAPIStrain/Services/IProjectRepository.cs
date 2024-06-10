using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;

namespace WebAPIStrain.Services
{
    public interface IProjectRepository
    {
        List<ProjectVM> GetAll();
        ProjectVM GetById(string id);
        ProjectVM Create(ProjectModel inputProject);
        bool Update(string id, ProjectModel inputProject);
        bool Delete(string id);
        List<ProjectVM> GetAllProjectByIdEmployee(string employeeId);
    }
}

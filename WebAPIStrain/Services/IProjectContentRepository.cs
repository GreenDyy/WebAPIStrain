using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;

namespace WebAPIStrain.Services
{
    public interface IProjectContentRepository
    {
        List<ProjectContentVM> GetAll();
        ProjectContentVM GetById(int id);
        ProjectContentVM Create(ProjectContentModel inputProjectContent);
        bool Update(int id, ProjectContentModel inputProjectContent);
        bool Delete(int id);
        bool UpdateStatusProject(string idProject, string status);
    }
}
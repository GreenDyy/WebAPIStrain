﻿using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;

namespace WebAPIStrain.Services
{
    public interface IContentWorkRepository
    {
        List<ContentWorkVM> GetAll();
        ContentWorkVM GetById(int id);
        ContentWorkVM Create(ContentWorkModel inputContentWork);
        bool Update(int id, ContentWorkModel inputContentWork);
        bool Delete(int id);
        bool UpdateStatusProjectContent(int idProjectContent, string status);
        bool UpdateFileSaveAndName(int idContentWork, byte[] fileSave, string fileName);
        bool UpdateStatusContentWork(int idContentWork, string result, string endDateActual);
        bool UpdateNotificationNull(int idContentWork);
        List<ContentWorkVM> GetAllByIdEmployee(string idEmployee);
        List<ContentWorkVM> GetAllByIdProjectContent(int idProjectContent);
    }
}
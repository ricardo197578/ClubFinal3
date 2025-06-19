using System;
using System.Collections.Generic;
using ClubMinimal.Models;
using ClubMinimal.Interfaces;
using ClubMinimal.Services;

namespace ClubMinimal.Services

{
    public interface ICarnetService
    {
        Carnet GetCarnet(int id);
        IEnumerable<Carnet> GetAllCarnets();
        void CreateCarnet(Carnet carnet);
        void UpdateCarnet(Carnet carnet);
        void DeleteCarnet(int id);
        Carnet GetCarnetBySocio(int socioId);
        void GenerateCarnetForSocio(int socioId, bool aptoFisico);

       
    }
}
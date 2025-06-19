using System;
using System.Collections.Generic;
using ClubMinimal.Models;
using ClubMinimal.Interfaces;
using ClubMinimal.Services;

namespace ClubMinimal.Services
{
    public interface IActividadService
    {
        List<Actividad> ObtenerActividadesDisponibles();
        List<Actividad> ObtenerActividadesParaNoSocios();
        Actividad ObtenerActividad(int id);
    }
}
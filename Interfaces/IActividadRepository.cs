using System.Collections.Generic;
using ClubMinimal.Models;

namespace ClubMinimal.Interfaces
{
    public interface IActividadRepository
    {
        void Agregar(Actividad actividad);
        List<Actividad> ObtenerTodas();
        Actividad ObtenerPorId(int id);
    }
}
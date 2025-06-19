using System.Collections.Generic;
using ClubMinimal.Models;

namespace ClubMinimal.Interfaces
{
    public interface INoSocioRepository
    {
        void Agregar(NoSocio noSocio);
        List<NoSocio> ObtenerTodos();

       
        NoSocio ObtenerPorId(int id);
        NoSocio BuscarPorDni(string dni);
    }
}
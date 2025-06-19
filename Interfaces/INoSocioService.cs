using System.Collections.Generic;
using ClubMinimal.Models;

namespace ClubMinimal.Interfaces
{
    public interface INoSocioService
    {
        void RegistrarNoSocio(string nombre, string apellido,string dni);
        List<NoSocio> ObtenerNoSocios();
        NoSocio BuscarPorDni(string dni); 
    }
}
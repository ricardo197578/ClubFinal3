using System;
using System.Collections.Generic;
using ClubMinimal.Models;

namespace ClubMinimal.Interfaces
{
    public interface ISocioRepository
    {
        void Agregar(Socio socio);
        List<Socio> ObtenerTodos();
        Socio ObtenerPorId(int id);
        Socio ObtenerPorDni(string Dni);
	//IEnumerable<Socio> ObtenerTodos();
    }
}
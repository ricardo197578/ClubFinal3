using System;

using ClubMinimal.Models;

namespace ClubMinimal.Interfaces
{
    public interface IPagoService
    {
        void ProcesarPago(int noSocioId, int actividadId, decimal monto, MetodoPago metodo);
    }
}
using System;

using ClubMinimal.Models;

namespace ClubMinimal.Interfaces
{
    public interface IPagoRepository
    {
        void RegistrarPago(Pago pago);
    }
}
//CODIGO COMENTADO POR IA
using System;
using System.Collections.Generic;
using ClubMinimal.Models;

namespace ClubMinimal.Interfaces
{
    public interface ICuotaRepository
    {
        /// <summary>
        /// Busca un socio activo por su DNI
        /// </summary>
        Socio BuscarSocioActivoPorDni(string dni);
        
        /// <summary>
        /// Registra el pago de una cuota y actualiza automáticamente la fecha de vencimiento
        /// </summary>
        void RegistrarPagoCuota(int socioId, decimal monto, DateTime fechaPago, MetodoPago metodo);
        
        /// <summary>
        /// Obtiene la fecha de vencimiento actual del socio
        /// </summary>
        DateTime ObtenerFechaVencimientoActual(int socioId);
        
        /// <summary>
        /// Actualiza manualmente la fecha de vencimiento de un socio
        /// </summary>
        void ActualizarFechaVencimiento(int socioId, DateTime nuevaFecha);
        
        /// <summary>
        /// Activa el estado de un socio
        /// </summary>
        void ActivarSocio(int socioId);
        
        /// <summary>
        /// Obtiene las cuotas por vencer hasta la fecha límite especificada
        /// </summary>
        IEnumerable<Cuota> ObtenerCuotasPorVencer(DateTime fechaLimite);
        
        /// <summary>
        /// Obtiene todas las cuotas de un socio específico
        /// </summary>
        IEnumerable<Cuota> ObtenerCuotasPorSocio(int socioId);
        
        /// <summary>
        /// Obtiene el valor actual de la cuota
        /// </summary>
        decimal ObtenerValorActualCuota();
    }
}

using System;
using System.Collections.Generic;
using ClubDeportivo.Interfaces;
using ClubDeportivo.Models;
using ClubDeportivo.Repositories;

namespace ClubDeportivo.Services
{
    public class SocioService : ISocioService
    {
        private readonly ISocioRepository _repository;

        public SocioService(ISocioRepository repository)
        {
            _repository = repository;
        }

        // Versión original 
        public void RegistrarSocio(string nombre, string apellido, string dni)
        {
            var socio = new Socio
            {
                Nombre = nombre,
                Apellido = apellido,
                Dni = dni,
                FechaInscripcion = DateTime.Now,
                FechaVencimientoCuota = DateTime.Now.AddMonths(1),
                EstadoActivo = true,
                Tipo = TipoSocio.Standard
            };
            _repository.Agregar(socio);
        }

        // Nueva versión acepta objeto Socio completo
        public void RegistrarSocio(Socio socio)
        {
            //if (socio == null) throw new ArgumentNullException(nameof(socio));
            if (socio == null) throw new ArgumentNullException("socio");
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(socio.Nombre))
                throw new ArgumentException("El nombre es requerido");
            if (string.IsNullOrWhiteSpace(socio.Apellido))
                throw new ArgumentException("El apellido es requerido");
            if (string.IsNullOrWhiteSpace(socio.Dni))
                throw new ArgumentException("El DNI es requerido");
            if (ExisteDni(socio.Dni))
                throw new InvalidOperationException("Ya existe un socio con este DNI");

            // Valores por defecto
            if (socio.FechaInscripcion == DateTime.MinValue)
                socio.FechaInscripcion = DateTime.Now;
            if (socio.FechaVencimientoCuota == DateTime.MinValue)
                socio.FechaVencimientoCuota = DateTime.Now.AddMonths(1);
            if (socio.Tipo == 0) // Valor por defecto del enum
                socio.Tipo = TipoSocio.Standard;

            socio.EstadoActivo = true; // Siempre activo al registrarse

            _repository.Agregar(socio);
        }

        public List<Socio> ObtenerSocios()
        {
            return _repository.ObtenerTodos();
        }

        public Socio GetSocio(int id)
        {
            return _repository.ObtenerPorId(id);
        }

        public Socio GetSocio(string dni)
        {
            return _repository.ObtenerPorDni(dni);
        }

        public bool ExisteDni(string dni)
        {
            return _repository.ObtenerPorDni(dni) != null;
        }

       
        public Socio ObtenerSocioPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }

       /* public void ActualizarVencimientoCuota(int socioId, DateTime nuevaFechaVencimiento)
        {
            var socio = _repository.ObtenerPorId(socioId);
            if (socio != null)
            {
                socio.FechaVencimientoCuota = nuevaFechaVencimiento;
                _repository.Actualizar(socio);
            }
        }*/

        public void RegistrarSocio(string nombre, string apellido, string dni, TipoSocio tipo)
        {
            var socio = new Socio
            {
                Nombre = nombre,
                Apellido = apellido,
                Dni = dni,
                FechaInscripcion = DateTime.Now,
                FechaVencimientoCuota = DateTime.Now.AddMonths(1),
                EstadoActivo = true,
                Tipo = tipo
            };
            this.RegistrarSocio(socio);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using ClubMinimal.Interfaces;
using ClubMinimal.Models;

namespace ClubMinimal.Services
{
    public class ActividadService : IActividadService
    {
        private readonly IActividadRepository _repository;

        public ActividadService(IActividadRepository repository)
        {
            _repository = repository;
        }

        public List<Actividad> ObtenerActividadesDisponibles()
        {
            return _repository.ObtenerTodas();
        }

        public List<Actividad> ObtenerActividadesParaNoSocios()
        {
            return _repository.ObtenerTodas()
                .Where(a => !a.ExclusivaSocios)
                .ToList();
        }

        public Actividad ObtenerActividad(int id)
        {
            return _repository.ObtenerPorId(id);
        }
    }
}
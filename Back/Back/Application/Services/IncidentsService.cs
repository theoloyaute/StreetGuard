using AutoMapper;
using Back.Api.Error;
using Back.Application.Services.Interfaces;
using Back.Domain.Models;
using Back.Domain.Repositories.Interfaces;

namespace Back.Application.Services;

public class IncidentsService : IIncidentsService
{
    private readonly IIncidentsRepository _incidentsRepository;
    private readonly IIncidentTypeRepository _incidentTypeRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IPowerIncidentTypeRepository _powerIncidentTypeRepository;
    private readonly IMapper _mapper;

    public IncidentsService(IIncidentsRepository incidentsRepository, IIncidentTypeRepository incidentTypeRepository,
        ICityRepository cityRepository, IPowerIncidentTypeRepository powerIncidentTypeRepository, IMapper mapper)
    {
        _incidentsRepository = incidentsRepository;
        _incidentTypeRepository = incidentTypeRepository;
        _cityRepository = cityRepository;
        _powerIncidentTypeRepository = powerIncidentTypeRepository;
        _mapper = mapper;
    }

    public Task<IEnumerable<Incidents>> ListAsync() => _incidentsRepository.ListAsync();

    public Task<Incidents?> FindAsync(int id) => _incidentsRepository.FindAsync(id);

    public async Task<Incidents> Add(Incidents incidents)
    {
        incidents.Id = _incidentsRepository.MaxId() + 1;
        var type = await _incidentTypeRepository.FindAsync(incidents.IncidentTypeId);
        if (type is null) throw new NotFoundException("Type d'incident incorrect !");
        var city = await _cityRepository.FindAsync(incidents.CityId);
        if (city is null) throw new NotFoundException("Ville incorrecte !");
        incidents.Date = DateOnly.FromDateTime(DateTime.Now);
        incidents.City = city;
        incidents.IncidentType = type;
        var incident = _mapper.Map<Incidents>(incidents);
        _incidentsRepository.Add(incident);
        await _incidentsRepository.SaveChangesAsync();
        return incident;
    }

    public async Task<Incidents> Update(Incidents incidents)
    {
        var incident = await _incidentsRepository.FindAsync(incidents.Id);
        if (incident is null) throw new NotFoundException("Incident introuvable !");
        var type = await _incidentTypeRepository.FindAsync(incidents.IncidentTypeId);
        if (type is null) throw new NotFoundException("Type d'incident incorrect !");
        incident.Date = incidents.Date;
        incident.Description = incidents.Description;
        incident.Longitude = incidents.Longitude;
        incident.Latitude = incidents.Latitude;
        incident.IncidentTypeId = incidents.IncidentTypeId;
        incident.IncidentType = type;
        _incidentsRepository.Update(incident);
        await _incidentsRepository.SaveChangesAsync();
        return incident;
    }

    public void Delete(int id)
    {
        var incident = _incidentsRepository.FindAsync(id).Result;
        if (incident is null) throw new NotFoundException("Incident introuvable !");
        _incidentsRepository.Remove(incident);
        _incidentsRepository.SaveChanges();
    }

    public async Task<List<Incidents>> FindIncidents(int powerId, double longitude, double latitude)
    {
        const double radius = 6371;
        
        var incidents = await _incidentsRepository.ListAsync();
        var powerIncidentTypes = await _powerIncidentTypeRepository.ListAsync();

        var filteredIncidents = new List<Incidents>();
        
        foreach(var incident in incidents)
        {
            var powerIncidentType = powerIncidentTypes.FirstOrDefault(x => x.PowerId == powerId && x.IncidentTypeId == incident.IncidentTypeId);
            var dLat = (incident.Latitude - latitude) * (Math.PI / 180);
            var dLon = (incident.Longitude - longitude) * (Math.PI / 180);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(latitude * (Math.PI / 180)) * Math.Cos(incident.Latitude * (Math.PI / 180)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = radius * c;
            if (distance <= 50 && powerIncidentType is not null)
            {
                filteredIncidents.Add(incident);
            }
        }

        return filteredIncidents;
    }
}
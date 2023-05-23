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
    private readonly IMapper _mapper;
    
    public IncidentsService(IIncidentsRepository incidentsRepository, IIncidentTypeRepository incidentTypeRepository, IMapper mapper)
    {
        _incidentsRepository = incidentsRepository;
        _incidentTypeRepository = incidentTypeRepository;
        _mapper = mapper;
    }
    
    public Task<IEnumerable<Incidents>> ListAsync() => _incidentsRepository.ListAsync();
    
    public Task<Incidents> FindAsync(int id) => _incidentsRepository.FindAsync(id);
    
    public async Task<Incidents> Add(Incidents incidents)
    {
        incidents.Id = _incidentsRepository.MaxId() + 1;
        var type = await _incidentTypeRepository.FindAsync(incidents.TypeIncidentId);
        if (type is null) throw new NotFoundException("Type d'incident incorrect !");
        var incident = _mapper.Map<Incidents>(incidents);
        _incidentsRepository.Add(incident);
        await _incidentsRepository.SaveChangesAsync();
        return incident;
    }
    
    public async Task<Incidents> Update(Incidents incidents)
    {
        var incident = await _incidentsRepository.FindAsync(incidents.Id);
        if (incident is null) throw new NotFoundException("Incident introuvable !");
        var type = await _incidentTypeRepository.FindAsync(incidents.TypeIncidentId);
        if (type is null) throw new NotFoundException("Type d'incident incorrect !");
        incident = _mapper.Map(incidents, incident);
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
}
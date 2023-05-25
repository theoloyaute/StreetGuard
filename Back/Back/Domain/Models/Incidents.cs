using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Back.Domain.Models;

[Table("incidents")]
public partial class Incidents
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("date")]
    public DateOnly Date { get; set; }

    [Column("description")]
    [StringLength(255)]
    public string Description { get; set; } = null!;

    [Column("longitude")]
    public double Longitude { get; set; }

    [Column("latitude")]
    public double Latitude { get; set; }

    [Column("incident_type_id")]
    public int IncidentTypeId { get; set; }

    [Column("city_id")]
    public int? CityId { get; set; }

    [ForeignKey("CityId")]
    [InverseProperty("Incidents")]
    public virtual City? City { get; set; }

    [ForeignKey("IncidentTypeId")]
    [InverseProperty("Incidents")]
    public virtual IncidentType IncidentType { get; set; } = null!;

    [InverseProperty("Incidents")]
    public virtual ICollection<Notification> Notification { get; set; } = new List<Notification>();
}

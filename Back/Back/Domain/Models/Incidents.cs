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
    [Precision(9, 6)]
    public decimal Longitude { get; set; }

    [Column("latitude")]
    [Precision(9, 6)]
    public decimal Latitude { get; set; }

    [Column("type_incident_id")]
    public int TypeIncidentId { get; set; }

    [InverseProperty("Incidents")]
    public virtual ICollection<Notification> Notification { get; set; } = new List<Notification>();

    [ForeignKey("TypeIncidentId")]
    [InverseProperty("Incidents")]
    public virtual IncidentType TypeIncident { get; set; } = null!;
}

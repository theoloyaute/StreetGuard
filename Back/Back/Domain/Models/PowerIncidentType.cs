using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Back.Domain.Models;

[Keyless]
[Table("power_incident_type")]
public partial class PowerIncidentType
{
    [Column("power_id")]
    public int PowerId { get; set; }

    [Column("incident_type_id")]
    public int IncidentTypeId { get; set; }

    [ForeignKey("IncidentTypeId")]
    public virtual IncidentType IncidentType { get; set; } = null!;

    [ForeignKey("PowerId")]
    public virtual Power Power { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Back.Domain.Models;

[Keyless]
[Table("power_type_incident")]
public partial class PowerTypeIncident
{
    [Column("power_id")]
    public int PowerId { get; set; }

    [Column("type_incident_id")]
    public int TypeIncidentId { get; set; }

    [ForeignKey("PowerId")]
    public virtual Power Power { get; set; } = null!;

    [ForeignKey("TypeIncidentId")]
    public virtual TypeIncident TypeIncident { get; set; } = null!;
}

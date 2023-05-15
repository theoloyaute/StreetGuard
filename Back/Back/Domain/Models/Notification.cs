using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Back.Domain.Models;

[Table("notification")]
public partial class Notification
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("date")]
    public DateOnly Date { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; } = null!;

    [Column("message")]
    [StringLength(255)]
    public string Message { get; set; } = null!;

    [Column("incidents_id")]
    public int? IncidentsId { get; set; }

    [ForeignKey("IncidentsId")]
    [InverseProperty("Notification")]
    public virtual Incidents? Incidents { get; set; }
}

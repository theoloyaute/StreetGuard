using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Back.Domain.Models;

[Table("city")]
public partial class City
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("longitude")]
    public double Longitude { get; set; }

    [Column("latitude")]
    public double Latitude { get; set; }

    [InverseProperty("City")]
    public virtual ICollection<Incidents> Incidents { get; set; } = new List<Incidents>();
}

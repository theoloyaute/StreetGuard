using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Back.Domain.Models;

[Table("users")]
public partial class Users
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("username")]
    [StringLength(255)]
    public string Username { get; set; } = null!;

    [Column("firstname")]
    [StringLength(255)]
    public string? Firstname { get; set; }

    [Column("lastname")]
    [StringLength(255)]
    public string? Lastname { get; set; }

    [Column("email")]
    [StringLength(255)]
    public string Email { get; set; } = null!;

    [Column("phone")]
    [StringLength(255)]
    public string Phone { get; set; } = null!;

    [Column("password")]
    [StringLength(255)]
    public string Password { get; set; } = null!;

    [Column("longitude")]
    public double? Longitude { get; set; }

    [Column("latitude")]
    public double? Latitude { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [Column("power_id")]
    public int? PowerId { get; set; }

    [ForeignKey("PowerId")]
    [InverseProperty("Users")]
    public virtual Power? Power { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; } = null!;
}

public partial class Login
{
    [Column("username")]
    [StringLength(255)]
    public string Username { get; set; }

    [Column("password")]
    [StringLength(255)]
    public string Password { get; set; }
}

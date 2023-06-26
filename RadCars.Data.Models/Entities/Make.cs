﻿namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

public class Make
{
    public Make()
    {
        this.Models = new HashSet<Model>();
    }

    [Key]
    public ushort Id { get; set; }

    [Required]
    [StringLength(1)]
    public string Name { get; set; } = null!;
    
    public virtual ICollection<Model> Models { get; set; }
}
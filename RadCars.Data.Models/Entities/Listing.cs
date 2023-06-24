namespace RadCars.Data.Models.Entities;

using System.ComponentModel.DataAnnotations;

using Enums;

public class Listing
{
    public Listing()
    {
        this.Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    //This should be the user's name
    //[Required]
    //public string AuthorName { get; set; }

    //[Required]
    //[ForeignKey(nameof(Make))]
    //public short MakeId { get; set; }

    //public Make Make { get; set; } = null!;

    //public Model Model { get; set; } = null!;

    [Required]
    public DateTime Year { get; set; }

    [Required]
    public long Mileage { get; set; }

    [Required]
    public string EngineModel { get; set; } = null!;

    [Required]
    public EngineType EngineType { get; set; }

    public string? EngineCode { get; set; }

    [Required]
    public string VinNumber { get; set; } = null!;
}
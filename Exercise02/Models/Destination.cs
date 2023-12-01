using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise02.Models;

[Table("destinations")]
public class Destination : IEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    // Collection for many-to-many relationship
    public List<Traveler> Travelers { get; set; }
    // Navigation property for many-to-many relationship
    public List<TravelerDestination> TravelerDestinations { get; set; }

    public override string ToString()
    {
        return $"{Id} - {Name}";
    }
}
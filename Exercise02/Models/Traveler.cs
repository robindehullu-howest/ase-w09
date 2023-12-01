using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise02.Models;

[Table("travelers")]
public class Traveler : IEntity
{
    [Key]
    public int Id {get; set;}

    [Required]
    [StringLength(100)]
    public string FullName {get; set;}

    public Passport Passport {get; set;}

    public List<Destination> Destinations {get; set;}

    public List<TravelerDestination> TravelerDestinations {get; set;}

    public override string ToString()
    {
        return $"{Id} - {FullName}";
    }
}
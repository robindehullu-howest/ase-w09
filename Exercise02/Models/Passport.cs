using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise03.Models;

[Table("passports")]
public class Passport
{
    [Key]
    public int Id {get; set;}

    [Required]
    [StringLength(9)]
    public string Passportnumber {get; set;}


    public int TravelerId {get; set;}
    public Traveler Traveler {get; set;}

    public override string ToString()
    {
        return $"{Id} - {Passportnumber}";
    }
}
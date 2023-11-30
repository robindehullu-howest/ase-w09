using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise03.Models;

[Table("guides")]
public class Guide
{
    [Key]
    public int Id {get; set;}

    [Required]
    [StringLength(200)]
    public string Name {get; set;}

    public List<Tour> Tours {get; set;}

    public override string ToString()
    {
        return $"{Id} - {Name}";
    }
}
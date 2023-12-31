using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise02.Models;

[Table("tours")]
public class Tour : IEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; }

    public int GuideId { get; set; }
    public Guide Guide { get; set; }

    public override string ToString()
    {
        return $"{Id} - {Title}";
    }
}
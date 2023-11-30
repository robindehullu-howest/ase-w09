using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise03.Models;

[Table("travelerdestinations")]
public class TravelerDestination
{
    public int TravelerId {get; set;}
    public Traveler Traveler {get; set;}

    public int DestinationId {get; set;}
    public Destination Destination {get; set;}
}
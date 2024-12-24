namespace ParkingSystem.Models;

public class Slot
{
    public int SlotNumber { get; set; }
    public Vehicle OccupiedVehicle { get; set; }
    public bool isOccupied => OccupiedVehicle != null;
}
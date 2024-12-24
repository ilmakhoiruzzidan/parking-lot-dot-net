using ParkingSystem.Models;

namespace ParkingSystem.Service;

public class ParkingService
{
    private readonly Dictionary<int, Slot> _parkingSlots;
    private readonly int _totalSlots;
    
    public ParkingService(int totalSlots)
    {
        _totalSlots = totalSlots;
        _parkingSlots = new Dictionary<int, Slot>();

        for (var i = 1; i <= totalSlots; i++)
        {
            _parkingSlots.Add(i, new Slot { SlotNumber = i });
        }
    }
    
    public string ParkVehicle(Vehicle vehicle)
    {
        foreach (var slot in _parkingSlots.Values)
        {
            if (slot.OccupiedVehicle != null) continue;
            slot.OccupiedVehicle = vehicle;
            return $"Allocated slot number: {slot.SlotNumber}";
        }
        return "Sorry, parking lot is full";
    }
    
    public string LeaveSlot(int slotNumber)
    {
        if (!_parkingSlots.ContainsKey(slotNumber) || _parkingSlots[slotNumber].OccupiedVehicle == null)
            return "Slot is already empty or invalid";
        _parkingSlots[slotNumber].OccupiedVehicle = null;
        return $"Slot number {slotNumber} is free";
    }
    
    public string GetStatus()
    {
        var status = "Slot\tNo.\t\tType\tRegistration No\tColour\n";
        foreach (var slot in _parkingSlots.Values)
        {
            if (slot.OccupiedVehicle == null) continue;
            var vehicle = slot.OccupiedVehicle;
            status += $"{slot.SlotNumber}\t{vehicle.RegistrationNumber}\t{vehicle.Type}\t{vehicle.Colour}\n";
        }
        return status;
    }
}
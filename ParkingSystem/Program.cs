
using ParkingSystem.Models;
using ParkingSystem.Service;

namespace ParkingSystem;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("===================================================");
        Console.WriteLine("Welcome to Parking System! (type one of menu below)");
        Console.WriteLine("create_parking_lot");
        Console.WriteLine("park");
        Console.WriteLine("leave");
        Console.WriteLine("status");
        Console.WriteLine("exit");
        
        ParkingService parkingService = null;
        
        while (true)
        {
            var input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input)) continue;

            var commands = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            switch (commands[0].ToLower())
            {
                    case "create_parking_lot":
                        if (commands.Length == 2 && int.TryParse(commands[1], out int totalSlots))
                        {
                            parkingService = new ParkingService(totalSlots);
                            Console.WriteLine($"Created a parking lot with {totalSlots} slots");
                        }
                        else
                        {
                            Console.WriteLine("Invalid command. Usage: create_parking_lot <total_slots>");
                        }
                        break;

                    case "park":
                        if (parkingService != null && commands.Length == 4)
                        {
                            var vehicle = new Vehicle
                            {
                                RegistrationNumber = commands[1],
                                Colour = commands[2],
                                Type = commands[3].ToLower() == "mobil" ? VehicleType.Car : VehicleType.Motorcycle
                            };
                            Console.WriteLine(parkingService.ParkVehicle(vehicle));
                        }
                        else
                        {
                            Console.WriteLine("Invalid command. Usage: park <registration_no> <colour> <type>");
                        }
                        break;

                    case "leave":
                        if (parkingService != null && commands.Length == 2 && int.TryParse(commands[1], out int slotNumber))
                        {
                            Console.WriteLine(parkingService.LeaveSlot(slotNumber));
                        }
                        else
                        {
                            Console.WriteLine("Invalid command. Usage: leave <slot_number>");
                        }
                        break;

                    case "status":
                        if (parkingService != null)
                        {
                            Console.WriteLine(parkingService.GetStatus());
                        }
                        else
                        {
                            Console.WriteLine("Parking lot is not created yet.");
                        }
                        break;

                    case "exit":
                        return;

                    default:
                        Console.WriteLine("Unknown command");
                        break;
                }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roommates.Repositories;
using Roommates.Models;
using Roommates.Constants;

namespace Roommates
{
    public class Program
    {


        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true;TrustServerCertificate=true;";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);

            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Show all rooms"):
                        List<Room> rooms = roomRepo.GetAll();
                        foreach (Room r in rooms)
                        {
                            Console.WriteLine($"{r.Name} has an Id of {r.Id} and a max occupancy of {r.MaxOccupancy}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Show all chores"):
                        List<Chore> chores = choreRepo.GetAll();
                        foreach (Chore r in chores)
                        {
                            Console.WriteLine($"{r.Name} has an Id of {r.Id}");
                        }
                        Console.Write("Press any key to continue");

                        Console.ReadKey();
                        break;
                    case ("Search for room"):
                        Console.Write("Room Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Room room = roomRepo.GetById(id);

                        Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for chore"):
                        Console.Write("Chore Id: ");
                        int choreId = int.Parse(Console.ReadLine());

                        Chore chore = choreRepo.GetById(choreId);

                        Console.WriteLine($"{chore.Id} - {chore.Name}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case (Options.AddRoom):
                        Console.Write("Room name: ");
                        string name = Console.ReadLine();

                        Console.Write("Max occupancy: ");
                        int max = int.Parse(Console.ReadLine());

                        Room roomToAdd = new Room(name, max);


                        roomRepo.Insert(roomToAdd);

                        Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;



                    case ("See unassigned chores"):
                        List<Chore> uChores = choreRepo.GetUnassignedChores();
                        Console.WriteLine("These are the chores that NO ONE IS DOING");
                        foreach (Chore c in uChores)
                        {
                            Console.WriteLine($"{c.Name} has an Id of {c.Id} AND NO ONE'S DOING IT");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;


                    case ("Add a chore"):
                        Console.Write("Chore name: ");
                        string choreName = Console.ReadLine();

                        //int max = int.Parse(Console.ReadLine());

                        Chore choreToAdd = new Chore()
                        {
                            Name = choreName
                        };

                        choreRepo.Insert(choreToAdd);

                        Console.WriteLine($"{choreToAdd.Name} has been added and assigned an Id of {choreToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Search for roommate"):
                        Console.Write("Roommate Id: ");
                        int rmId = int.Parse(Console.ReadLine());

                        Roommate rm = roommateRepo.GetById(rmId);

                        Console.WriteLine($"{rm.FirstName} occupies the {rm.Room.Name} and pays {rm.RentPortion} dabloons in rent.");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Assign chore to roommate"):
                        List<Chore> choresToAssign = choreRepo.GetUnassignedChores();
                        foreach (Chore c in choresToAssign)
                        {
                            Console.WriteLine($"{c.Id}: {c.Name}");
                        }
                        Console.Write("Which chore would you like to assign? ");
                        int choreToAssignId = int.Parse(Console.ReadLine());
                        Chore assignedChore = choreRepo.GetById(choreToAssignId);

                        List<Roommate> roomies = roommateRepo.GetAll();
                        foreach (Roommate r in roomies)
                        {
                            Console.WriteLine($"{r.Id}: {r.FirstName}");
                        }
                        Console.Write("Which roommate would you like to assign the chore to? ");
                        int rmToAssignId = int.Parse(Console.ReadLine());
                        Roommate assignedRoomie = roommateRepo.GetById(rmToAssignId);

                        choreRepo.AssignChore(rmToAssignId, choreToAssignId);

                        Console.WriteLine($"{assignedRoomie.FirstName} has been assigned to {assignedChore.Name}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Show chore counts for all roommates"):
                        List<RoomateChoreCount> choreCounts = choreRepo.GetChoreCounts();
                        foreach (RoomateChoreCount choreCount in choreCounts)
                        {
                            Console.WriteLine($"{choreCount.roomate.FirstName} {choreCount.roomate.LastName} has been assigned {choreCount.choreCount} total chores.");
                        }
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Update a room"):
                        List<Room> roomOptions = roomRepo.GetAll();
                        foreach (Room r in roomOptions)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }

                        Console.Write("Which room would you like to update? ");
                        int selectedRoomId = int.Parse(Console.ReadLine());
                        Room selectedRoom = roomOptions.FirstOrDefault(r => r.Id == selectedRoomId);

                        Console.Write("New Name: ");
                        selectedRoom.Name = Console.ReadLine();

                        Console.Write("New Max Occupancy: ");
                        selectedRoom.MaxOccupancy = int.Parse(Console.ReadLine());

                        roomRepo.Update(selectedRoom);

                        Console.WriteLine("Room has been successfully updated");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for a roommate"):
                        Console.Write("Enter roommate id: ");
                        int rId = int.Parse(Console.ReadLine());
                        Roomate searchedRoomate = roomateRepository.GetById(rId);
                        if (searchedRoomate != null)
                        {
                            Console.WriteLine($"{searchedRoomate.FirstName} - Rent Portion: {searchedRoomate.RentPortion} - Room: {searchedRoomate.Room.Name}");

                        }
                        else
                        {
                            Console.WriteLine("No such roommate");
                        }
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Exit"):
                        runProgram = false;
                        break;


                    case ("Delete a room"):
                        List<Room> roomOptions = roomRepo.GetAll();
                        foreach (Room r in roomOptions)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name} Max Occupany({r.MaxOccupancy})");
                        }
                        Console.Write("Which room would you like to delete?");
                        int selectedRoomId = int.Parse(Console.ReadLine());
                        Room selectedRoom = roomOptions.FirstOrDefault(r => r.Id == selectedRoomId);

                        Console.WriteLine("Room has been successfully deleted");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();



                    case ("Exit"):
                        runProgram = false;
                        break;
                }

            }
        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
            {
                "Show all rooms",
                "Show all chores",
                "Search for chore",
                "Search for room",
                Options.AddRoom,
                "Add a chore",
                "See unassigned chore",
                "Search for roommate",
                "Assign chore to roommate",
                "Show chore counts for all roommates",
                Options.UpdateRoom,
                "Exit"
            };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }
            //GetMenuSelection is returning a number of the options
            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }
        }
    }
}


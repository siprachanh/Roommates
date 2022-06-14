using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roommates.Models;
using Microsoft.Data.SqlClient;

namespace Roommates.Repositories
{
    public class RoommateRepository : BaseRepository
    {
        public RoommateRepository(string connectionString) : base(connectionString) { }
        public Roommate GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Roommate.Id, rm.FirstName, r.Name, rm.RentPortion FROM Roommate rm LEFT JOIN
                        Room r ON  r.Id WHERE rm.Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);



                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Roommate roommate = null;

                        // If we only expect a single row back from the database, we don't need a while loop.
                        if (reader.Read())
                        {
                            roommate = new Roommate()
                            {
                                Id = id,
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion")),
                                MovedInDate = reader.GetDateTime(reader.GetOrdinal("MoveInDate")),
                                Room = new Room()
                                {
                                    Name = reader.GetString(reader.GetOrdinal("Name"))
                                }



                            };
                        }
                        return roommate;
                    }

                }
            }
        }
        public List<Roommate> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Roommate.Id, FirstName, LastName, RentPortion, MoveInDate, Room.Id AS RoomId, Room.Name AS RoomName, Room.MaxOccupancy
                                        FROM Roommate
                                        JOIN Room ON Room.Id = Roommate.RoomId;";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Roommate> roommateList = new List<Roommate>();
                        while (reader.Read())

                        {
                            roommateList.Add(new Roommate()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion")),
                                MovedInDate = reader.GetDateTime(reader.GetOrdinal("MoveInDate")),
                                Room = new Room()

                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("RoomId"));
                            Name = reader.GetString(reader.GetOrdinal("RoomName"));
                            MaxOccupancy = reader.GetString(reader.GetOrdinal("MaxOccuupancy"));

                             }
                            };
                    //}
                    //            {
                    //                Id = idValue,
                    //                FirstName = firstNameValue,
                    //                LastName = lastNameValue
                    //            };
                    //            roommateList.Add(rm);
                                 }
                             return roommateList;
                         }
                    }
                }
            }
        }
    }



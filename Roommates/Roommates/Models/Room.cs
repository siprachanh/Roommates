using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Models
{
    // C# representation of the Room table
    public class Room
    {
       public Room( string name, int maxOccupancy)
        {
           
            Name = name;
            MaxOccupancy = maxOccupancy;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxOccupancy { get; set; }
    }
}
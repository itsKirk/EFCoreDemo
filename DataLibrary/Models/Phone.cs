using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public OS OS { get; set; }
    }
}

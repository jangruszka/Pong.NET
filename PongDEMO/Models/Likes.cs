using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PongDEMO.Models
{
    public class Likes
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int Yes { get; set; }
        public int No { get; set; }

    }
}

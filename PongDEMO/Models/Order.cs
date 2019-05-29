using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PongDEMO.Models
{
    public class Order
    {
        public int Id { get; set; }
    
        //It's just some mock model.
        //In real app it would be more complex, with data referenced by foreign keys, not hardcoded into model.
        //Splitting Order view into granular components might not always have sense - ex. if single parts of placed order will never be changed
       
        public string UserName { get; set; }
        public string UserMail { get; set; }
        public string Adress { get; set; }
        public string ProductName { get; set; }
        public string Color { get; set; }
        public string DeliveryMethod { get; set; }
        public int Price { get; set; }
    }
}

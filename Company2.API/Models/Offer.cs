using System;
using System.ComponentModel.DataAnnotations;

namespace Company2.API.Models
{
    public class Offer
    {
        [Required]
        public string Consignee { get; set; }
        [Required]
        public string Consignor { get; set; }
        [Required]
        public int[] Cartons { get; set; }
    }
}


using System;
using System.ComponentModel.DataAnnotations;

namespace Company1.API.Models
{
    public class Deal
    {
        [Required]
        public string ContactAddress { get; set; }
        [Required]
        public string WarehouseAddress { get; set; }
        [Required]
        public int[] PackageDimensions { get; set; }
    }
}


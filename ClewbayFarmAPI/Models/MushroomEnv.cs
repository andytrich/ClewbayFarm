using System;
using System.ComponentModel.DataAnnotations;

namespace ClewbayFarmAPI.Models;

public partial class MushroomEnv
    {
        [Key]
        public int Id { get; set; } // Auto-incrementing primary key

        [Required]
        public double Temperature { get; set; } // Temperature in degrees

        [Required]
        public double Humidity { get; set; } // Humidity as a percentage

        [Required]
        public int CO2Level { get; set; } // CO2 level in ppm

        [Required]
        public DateTime RecordedDateTime { get; set; } // UTC time of insertion
    }

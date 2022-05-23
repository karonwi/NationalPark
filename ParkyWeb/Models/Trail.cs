﻿using System.ComponentModel.DataAnnotations;

namespace ParkyWeb.Models
{
    public class Trail
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Distance { get; set; }
        
        [Required]
        public string Elevation { get; set; }

        public DifficultyType Difficulty { get; set; }

        public enum  DifficultyType { Easy, Moderate, Difficult, Expert }
        [Required]

        public int NationalParkId { get; set; }

        public NationalPark NationalPark { get; set; }
    }
}

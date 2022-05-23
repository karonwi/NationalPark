﻿using System.ComponentModel.DataAnnotations;
using static ParkyAPI.Models.Trail;

namespace ParkyAPI.Models.Dtos
{
    public class TrailCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Distance { get; set; }

        public DifficultyType Difficulty { get; set; }
        [Required]
   
        public int NationalParkId { get; set; }
        [Required]
        public string Elevation { get; set; }


    }
}

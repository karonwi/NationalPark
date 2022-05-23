﻿using System.ComponentModel.DataAnnotations;
using static ParkyAPI.Models.Trail;

namespace ParkyAPI.Models.Dtos
{
    public class TrailDto
    {
       
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Distance { get; set; }
        [Required]
        public string Elevation { get; set; }

        public DifficultyType Difficulty { get; set; }
        [Required]
   
        public int NationalParkId { get; set; }

        public NationalParkDto NationalPark { get; set; }
    }
}

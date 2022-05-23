using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkyAPI.Models
{
    public class Trail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Distance { get; set; }

        [Required]
        public string Elevation { get; set; }

        public enum DifficultyType { Easy, Moderate , Difficult , Expert }

        //Variable to store the difficulty type 
        public DifficultyType Difficulty { get; set; }

        [Required]
        //Foreign key reference of the national park here
        public int NationalParkId { get; set; }
         
        [ForeignKey("NationalParkId")]//The variable of the foreign key

        public NationalPark NationalPark { get; set; }//the table the foreign key binds to

        public DateTime DateCreated { get; set; }
    }
}

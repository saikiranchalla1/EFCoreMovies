﻿using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreMovies.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? DateOfBirth { get; set; }
    }
}
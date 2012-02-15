using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CMajor.Models {
    public class Album {
         public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string MyanmarName { get; set; }

        public ICollection<Song> Songs { get; set; }
    }
}
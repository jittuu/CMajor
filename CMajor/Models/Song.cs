using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CMajor.Models {
    public class Song {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string MyanmarTitle { get; set; }

        [Required]
        public string Content { get; set; }

        public ICollection<Album> Albums { get; set; }

        public ICollection<Artist> Artists { get; set; }
    }
}
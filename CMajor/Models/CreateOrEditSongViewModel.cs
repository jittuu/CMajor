using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CMajor.Models {
    public class CreateOrEditSongViewModel {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string MyanmarTitle { get; set; }

        [Required]
        public string Lyric { get; set; }

        public IEnumerable<int> AlbumIds { get; set; }

        public MultiSelectList Albums { get; set; }

        public IEnumerable<int> ArtistIds { get; set; }

        public MultiSelectList Artists { get; set; }
    }
}
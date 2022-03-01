using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumsApp.Models.ViewModels
{
    public class AlbumViewModel
    {
        public List<Album> Albums { get; set; }

        public string StudioName { get; set; }
    }
}

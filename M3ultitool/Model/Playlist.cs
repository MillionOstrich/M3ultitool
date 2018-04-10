using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3ultitool.Model
{
    public class Playlist
    {
        public bool RelativePaths { get; set; }
        public string FullPath { get; set; }
        public ICollection<PlaylistItem> Items { get; set; }

        public Playlist()
        {
            FullPath = null;
            Items = new List<PlaylistItem>();
            RelativePaths = true;
        }
        public Playlist(string path)
        {
            FullPath = path;
            Items = new List<PlaylistItem>();
            RelativePaths = true;
        }
        public Playlist(string path, ICollection<PlaylistItem> items)
        {
            FullPath = path;
            Items = items ?? throw new ArgumentNullException();
            RelativePaths = true;
        }

        public Playlist DeepCopy()
        {
            List<PlaylistItem> copy = new List<PlaylistItem>(Items.Count);
            foreach(var x in Items) { copy.Add(x.Copy()); }
            return new Playlist(FullPath, copy);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3ultitool.Model
{
    class PlaylistItemFactory
    {
        public PlaylistItem FromFile(string path)
        {
            PlaylistItem item = null;
            if(System.IO.File.Exists(path))
            {
                try
                {
                    using (var file = TagLib.File.Create(path))
                    {
                        item = new PlaylistItem(path, file.Tag);
                    }
                }
                catch
                {
                    //In cases of unsupported file types or unreadable files, just don't add them to the playlist.
                }
            }
            return item;
        }

        public IEnumerable<PlaylistItem> FromPathList(StringCollection paths)
        {
            foreach (string path in paths)
            {
                if (System.IO.Directory.Exists(path))
                {
                    foreach (string file in System.IO.Directory.EnumerateFiles(path, "*.*", System.IO.SearchOption.AllDirectories))
                    {
                        var result = FromFile(file);
                        if (result != null) yield return result; 
                    }
                }
                else
                {
                    var result = FromFile(path);
                    if (result != null) yield return result;
                }
            }
        }

    }
}

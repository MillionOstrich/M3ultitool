using System;
using TagLib;

namespace M3ultitool.Model
{
    public class PlaylistItem
    {
        private string _path;
        private Tag _tags;

        public string FullPath { get { return _path; } }
        public Tag Tags { get { return _tags; } }

        public PlaylistItem(string path)
        {
            _path = path ?? throw new ArgumentNullException();
            _tags = null;
        }
        public PlaylistItem(string path, Tag tags)
        {
            _path = path ?? throw new ArgumentNullException();
           _tags = tags;
        }

        public PlaylistItem Copy()
        {
            return new PlaylistItem(_path, _tags);
        }
    }
}

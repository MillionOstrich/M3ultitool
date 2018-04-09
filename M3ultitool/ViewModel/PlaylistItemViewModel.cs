using System;
using System.IO;
using M3ultitool.Model;

namespace M3ultitool.ViewModel
{
    public class PlaylistItemViewModel : IViewModel<PlaylistItem>
    {
        private PlaylistItem _item;
        public PlaylistItem Model { get { return _item; } }

        public string FullPath
        {
            get
            {
                return _item.FullPath;
            }
        }
        public string Title
        {
            get
            {
                return _item.Tags?.Title ?? Path.GetFileNameWithoutExtension(_item.FullPath);
             }
        }
        public string Album
        {
            get
            {
                return _item.Tags?.Album ?? "";
            }
        }
        public string Artists
        {
            get
            {
                return _item.Tags?.JoinedPerformers ?? _item.Tags?.JoinedAlbumArtists ?? "";
            }
        }
        public string Comment
        {
            get
            {
                return _item.Tags?.Comment ?? "";
            }
        }
        public string Genres
        {
            get
            {
                return _item.Tags?.JoinedGenres ?? "";
            }
        }

        public PlaylistItemViewModel(PlaylistItem item)
        {
            _item = item ?? throw new ArgumentNullException();
        }
    }
}

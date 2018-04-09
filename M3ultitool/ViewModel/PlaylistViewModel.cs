using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using M3ultitool.Model;

namespace M3ultitool.ViewModel
{
    public class PlaylistViewModel : INotifyPropertyChanged, IViewModel<Playlist>
    {
        private Playlist _playlist;
        private VMCollection<PlaylistItem, PlaylistItemViewModel> _itemsProxy;

        public event PropertyChangedEventHandler PropertyChanged;
        public Playlist Model { get { return _playlist; } }

        public VMCollection<PlaylistItem, PlaylistItemViewModel> Items { get { return _itemsProxy; } }
        public string FullPath {
            get { return _playlist.FullPath ?? Path.GetFullPath("./untitled.m3u"); }
            set { _playlist.FullPath = value; OnChanged(); OnChanged("Name"); }
        }
        public string Name
        {
            get { return Path.GetFileNameWithoutExtension(FullPath); }
        }

        public PlaylistViewModel()
        {
            _playlist = new Playlist();
            _itemsProxy = new VMCollection<PlaylistItem, PlaylistItemViewModel>(_playlist.Items);
        }
        public PlaylistViewModel(Playlist playlist)
        {
            _playlist = playlist ?? throw new ArgumentNullException();
            _itemsProxy = new VMCollection<PlaylistItem, PlaylistItemViewModel>(playlist.Items);
        }

        private void OnChanged([CallerMemberName] string prop="")
        { 
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

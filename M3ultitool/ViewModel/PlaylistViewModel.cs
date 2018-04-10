using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using GongSolutions.Wpf.DragDrop;
using M3ultitool.Model;

namespace M3ultitool.ViewModel
{
    public class PlaylistViewModel : INotifyPropertyChanged, IViewModel<Playlist>, IDropTarget
    {
        private bool _unsavedChanges;
        private Playlist _playlist;
        private VMCollection<PlaylistItem, PlaylistItemViewModel> _itemsProxy;

        public event PropertyChangedEventHandler PropertyChanged;
        public VMCollection<PlaylistItem, PlaylistItemViewModel> Items
        {
            get { return _itemsProxy; }
        }
        public string FullPath
        {
            get { return _playlist.FullPath; }
            set { _playlist.FullPath = value; UnsavedChanges = true; OnChanged(); }
        }
        public bool UnsavedChanges
        {
            get { return _unsavedChanges; }
            set { _unsavedChanges = value; OnChanged(); }
        }
        public bool RelativePaths
        {
            get { return _playlist.RelativePaths; }
            set { _playlist.RelativePaths = value; OnChanged(); }
        }
        public Playlist Model
        {
            get { return _playlist; }
        }

        public PlaylistViewModel()
        {
            _playlist = new Playlist();
            _itemsProxy = new VMCollection<PlaylistItem, PlaylistItemViewModel>(_playlist.Items, x => new PlaylistItemViewModel(x));
            _itemsProxy.CollectionChanged += (object o, NotifyCollectionChangedEventArgs e) => UnsavedChanges = true;
            _unsavedChanges = false;
        }
        public PlaylistViewModel(Playlist playlist)
        {
            _playlist = playlist ?? throw new ArgumentNullException();
            _itemsProxy = new VMCollection<PlaylistItem, PlaylistItemViewModel>(playlist.Items, x => new PlaylistItemViewModel(x));
            _itemsProxy.CollectionChanged += (object o, NotifyCollectionChangedEventArgs e) => UnsavedChanges = true;
            _unsavedChanges = false;
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is DataObject obj && obj.ContainsFileDropList())
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                dropInfo.Effects = DragDropEffects.Copy;
            }
            else
            {
                var handler = new DefaultDropHandler();
                handler.DragOver(dropInfo);
            }
        }
        public void Drop(IDropInfo dropInfo)
        {
            if (dropInfo.Data is DataObject obj && obj.ContainsFileDropList())
            {
                int index = dropInfo.InsertIndex;
                var factory = new PlaylistItemFactory();
                foreach(var item in factory.FromPathList(obj.GetFileDropList()))
                {
                    _itemsProxy.Insert(index, new PlaylistItemViewModel(item));
                    ++index;
                }
            }
            else
            {
                var handler = new DefaultDropHandler();
                handler.Drop(dropInfo);
            }
        }

        private void OnChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

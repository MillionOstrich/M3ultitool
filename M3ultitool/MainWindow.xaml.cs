using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using M3ultitool.Model;
using M3ultitool.ViewModel;
using Microsoft.Win32;

namespace M3ultitool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SaveFileDialog _saveDialog;
        private OpenFileDialog _openDialog;

        public MainWindow()
        {
            InitializeComponent();
            _saveDialog = new SaveFileDialog()
            {
                FileName = PlaylistViewModel.DefaultName,
                DefaultExt = ".m3u",
                Filter = "Playlists (.m3u)|*.m3u",
                Title = "Save playlist"
            };
            _openDialog = new OpenFileDialog()
            {
                Filter = "Playlists (.m3u)|*.m3u",
                Title = "Open playlist"
            };
        }

        private bool CheckUnsavedChanges()
        {
            if(mainPlaylist.UnsavedChanges)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save changes?", "Unsaved Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if(result == MessageBoxResult.Yes)
                {
                    return SaveChanges(false);
                }
                else
                {
                    return result == MessageBoxResult.No;
                }
            }
            return true;
        }

        private bool SaveChanges(bool _saveAs)
        {
            bool exists = mainPlaylist.FullPath != null && File.Exists(mainPlaylist.FullPath);
            if (!exists || _saveAs)
            {
                _saveDialog.FileName = mainPlaylist.FullPath;
                bool? result = _saveDialog.ShowDialog();
                if(result != true) { return false; }
                mainPlaylist.FullPath = _saveDialog.FileName;
            }
            mainPlaylist.UnsavedChanges = false;
            return true;
        }

        private void OnClickNew(object sender, RoutedEventArgs e)
        {
            if(CheckUnsavedChanges())
            {
                mainPlaylist = new PlaylistViewModel();
                DataContext = mainPlaylist;
            }
        }
        private void OnClickOpen(object sender, RoutedEventArgs e)
        {
            if (CheckUnsavedChanges())
            {
                bool? opened = _openDialog.ShowDialog();
                if(opened == true)
                {
                    //TODO
                }
            }
        }
        private void OnClickSave(object sender, RoutedEventArgs e)
        {
            SaveChanges(false);
        }
        private void OnClickSaveAs(object sender, RoutedEventArgs e)
        {
            SaveChanges(true);
        }
        private void OnClickQuit(object sender, RoutedEventArgs e)
        {
            if (CheckUnsavedChanges())
            {
                Close();
            }
            e.Handled = true;
        }
        private void OnDeleteDown(object sender, KeyEventArgs e)
        {
            if (sender is ListView lv && (e.Key == Key.Delete || e.Key == Key.Back))
            {
                e.Handled = true;
                if (lv.SelectedItems.Count == 0) { return; }
                var collection = lv.ItemsSource as ICollection<PlaylistItemViewModel>;
                var toDelete = new List<PlaylistItemViewModel>(lv.SelectedItems.Count);
                foreach (PlaylistItemViewModel vm in lv.SelectedItems) { toDelete.Add(vm); }
                foreach (PlaylistItemViewModel vm in toDelete) { collection.Remove(vm); }
            }
        }
    }
}

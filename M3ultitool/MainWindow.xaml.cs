using M3ultitool.Model;
using M3ultitool.ViewModel;
using System.Windows;

namespace M3ultitool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            for(int i=0; i<10; ++i)
            {
                mainPlaylist.Model.Items.Add(new PlaylistItem("./test" + i + ".mp3"));
            }
            mainPlaylist.Items.Refresh(x => new PlaylistItemViewModel(x));
        }

    }
}

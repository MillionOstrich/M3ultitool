using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace M3ultitool.ViewModel
{
    public class AppTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is PlaylistViewModel vm)
            {
                string path = Path.GetFileName(vm.FullPath ?? PlaylistViewModel.DefaultName);
                return vm.UnsavedChanges ? path + "*" : path;
            }
            return "";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

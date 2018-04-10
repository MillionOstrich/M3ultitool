using System;
using System.IO;

namespace M3ultitool
{
    public class PathUtils
    {
        public static string WithTrailingSeperator(string _path)
        {
            if(!_path.EndsWith(Path.DirectorySeparatorChar.ToString()) && Directory.Exists(_path))
            {
                return _path + Path.DirectorySeparatorChar;
            }
            else
            {
                return _path;
            }
        }

        public static string RelativePath(string _path, string _to)
        {
            var path = new Uri(WithTrailingSeperator(_path));
            var to = new Uri(WithTrailingSeperator(_to));

            if (path.Scheme != to.Scheme) { return _path; }
            string relative = Uri.UnescapeDataString(to.MakeRelativeUri(path).ToString());
            return relative.Replace('/', Path.DirectorySeparatorChar);
        }
    }
}

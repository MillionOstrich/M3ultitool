using System;
using System.IO;
using System.Threading.Tasks;

namespace M3ultitool.Model
{
    public class PlaylistReader : IDisposable
    {
        private string _relativeTo;
        private StreamReader _reader;
        bool _wasRelative;

        public bool WasRelativePath { get { return _wasRelative; } }

        public PlaylistReader(string path)
        {
            _reader = new StreamReader(new FileStream(path, FileMode.Open));
            _relativeTo = PathUtils.WithTrailingSeperator(Path.GetDirectoryName(path));
            _wasRelative = false;
        }

        public string NextPath()
        {
            string path = null;
            while(!_reader.EndOfStream)
            {
                path = _reader.ReadLine().Trim();
                if (path.Length > 0 && !path.StartsWith("#")) { break; }
            }

            if (path != null && !Path.IsPathRooted(path))
            {
                return _relativeTo + path;
            }
            else
            {
                return path;
            }
        }

        public async Task<string> NextPathAsync()
        {
            string path = null;
            while (!_reader.EndOfStream)
            {
                path = (await _reader.ReadLineAsync()).Trim();
                if (path.Length > 0 && !path.StartsWith("#")) { break; }
            }
            _wasRelative = Path.IsPathRooted(path);
            return _wasRelative ? path : _relativeTo + path;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _reader.Dispose();
                    _reader = null;
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}

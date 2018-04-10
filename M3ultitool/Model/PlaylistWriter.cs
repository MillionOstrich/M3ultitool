using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3ultitool.Model
{
    public class PlaylistWriter : IDisposable
    {
        private string _relativeTo;
        private StreamWriter _writer;

        public PlaylistWriter(string path, bool _relative)
        {
            _writer = new StreamWriter(new FileStream(path, FileMode.Create));
            if(_relative)
            {
                _relativeTo = Path.GetDirectoryName(Path.GetFullPath(path));
            }
            else
            {
                _relativeTo = null;
            }
            
            WriteIntro();
        }

        public void Write(PlaylistItem _item)
        {
            if(_relativeTo != null)
            {
                _writer.WriteLine(PathUtils.RelativePath(_item.FullPath, _relativeTo));
            }
            else
            {
                _writer.WriteLine(_item.FullPath);
            }
        }

        public async Task WriteAsync(PlaylistItem _item)
        {
            if (_relativeTo != null)
            {
                await _writer.WriteLineAsync(PathUtils.RelativePath(_item.FullPath, _relativeTo));
            }
            else
            {
                await _writer.WriteLineAsync(_item.FullPath);
            }
        }

        private void WriteIntro()
        {
            _writer.WriteLine("#EXTM3U");
            _writer.WriteLine();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _writer.Dispose();
                    _writer = null;
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

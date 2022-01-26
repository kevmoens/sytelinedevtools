using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SyteLineDevTools.SyteLine.Connections
{
    public class FileSystemConnection : IFileSystemConnection, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private string _name;
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        private string _baseDir;
        public string BaseDir { get => _baseDir; set { _baseDir = value; OnPropertyChanged(); } }
        public string Type => "FileSystem";

    }
}

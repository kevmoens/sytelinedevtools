using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SyteLineDevTools.SyteLine.Connections
{
    public class IDOConnection : IIDOConnection, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private string _url;
        public string URL { get => _url; set { _url = value; OnPropertyChanged(); } }
        private string _username;
        public string UserName { get => _username; set { _username = value; OnPropertyChanged(); } }
        private string _password;
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }
        private string _configuration;
        public string Configuration { get => _configuration; set { _configuration = value; OnPropertyChanged(); } }
        private string _site;
        public string Site { get => _site; set { _site = value; OnPropertyChanged(); } }
        private bool _useWindowAuthentication;
        public bool UseWindowAuthentication { get => _useWindowAuthentication; set { _useWindowAuthentication = value; OnPropertyChanged(); } }
        private string _name;
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }

        public string Type => "IDO";
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SyteLineDevTools.SyteLine.Connections
{
    public class SLSQLConnection : ISLSQLConnection, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private string _server;
        public string Server { get => _server; set { _server = value; OnPropertyChanged(); } }
        private string _database;
        public string Database { get => _database; set { _database = value; OnPropertyChanged(); } }
        private string _userId;
        public string UserId { get => _userId; set { _userId = value; OnPropertyChanged(); } }
        private string _password;
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }
        private bool _integratedSecurity;
        public bool IntegratedSecurity { get => _integratedSecurity; set { _integratedSecurity = value; OnPropertyChanged(); } }
        private string _name;
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }

        public string Type => "SLSQL";
        private string _site;
        public string Site { get => _site; set { _site = value; OnPropertyChanged(); } }
    }
}

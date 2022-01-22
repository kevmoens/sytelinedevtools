using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyteLineDevTools.MVVM.Services
{
     public interface IMessageBox
    {
        string Show(string message, string caption, string buttons, string images);
        string Show(string message, string caption, string buttons);
        string Show(string message);
    }
}

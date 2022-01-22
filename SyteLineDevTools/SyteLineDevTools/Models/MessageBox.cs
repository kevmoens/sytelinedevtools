using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SyteLineDevTools.MVVM.Services;

namespace SyteLineDevTools.Models
{
    public class MessageBox : IMessageBox
    {
        public string Show(string Message, string Caption, string Buttons, string Images)
        {
            MessageBoxButton btn = MessageBoxButton.OK;
            MessageBoxImage img = MessageBoxImage.None;
            Enum.TryParse<MessageBoxButton>(Buttons, out btn);
            Enum.TryParse<MessageBoxImage>(Images, out img);
            if (string.IsNullOrEmpty(Caption))
                Caption = "SyteLineDevTools";
            var result = System.Windows.MessageBox.Show(Message, Caption, btn, img);
            return result.ToString();
        }

        public string Show(string Message, string Caption, string Buttons)
        {
            return Show(Message, Caption, Buttons, "");
        }

        public string Show(string Message)
        {
            return Show(Message, "", "", "");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyteLineDevTools.MVVM.Services
{
    public interface IView
    {
        object DataContext { get; set; }
    }
}

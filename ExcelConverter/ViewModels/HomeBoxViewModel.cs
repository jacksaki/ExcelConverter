using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelConverter.ViewModels
{
    public class HomeBoxViewModel : ViewModelBase
    {
        public MainWindowViewModel Parent { get; }
        public HomeBoxViewModel(MainWindowViewModel parent)
        {
            this.Parent = parent;
        }
    }
}

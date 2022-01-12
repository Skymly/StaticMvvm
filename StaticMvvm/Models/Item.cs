using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticMvvm.Models
{
    public class Item : Prism.Mvvm.BindableBase
    {
        private string name;
        private int money;

        public string Name { get => name; set => SetProperty(ref name, value); }
        public int Money { get => money; set => SetProperty(ref money, value); }
    }
}

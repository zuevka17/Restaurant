
using Avalonia.Controls;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReactiveUI;
using Restaurant.Models;
using Restaurant.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels
{
    public class EditWindowMenuViewModel : ViewModelBase
    {
        private Models.Menu _menus;
        public Models.Menu Menus
        {
            get => _menus;
            set => this.RaiseAndSetIfChanged(ref _menus, value);
        }
        public EditWindowMenuViewModel() { }
        public EditWindowMenuViewModel(Models.Menu menu) : this()
        {
            this.Menus = menu;
        }
    }
}

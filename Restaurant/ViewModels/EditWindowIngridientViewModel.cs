
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using Restaurant.Models;
using Restaurant.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels
{
    public class EditWindowIngridientViewModel : ViewModelBase
    {
        private Ingridient _ingridient;
        public Ingridient ingridient
        {
            get => _ingridient;
            set => this.RaiseAndSetIfChanged(ref _ingridient, value);
        }
        public EditWindowIngridientViewModel() { }
        public EditWindowIngridientViewModel(Ingridient ingridient) : this()
        {
            this.ingridient = ingridient;
        }
    }
}

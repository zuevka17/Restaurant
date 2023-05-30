using Avalonia.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using Restaurant.Models;
using Restaurant.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace Restaurant.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Ingridient> _ingridients;
        public ObservableCollection<Ingridient> Ingridients
        {
            get => _ingridients;
            set => this.RaiseAndSetIfChanged(ref _ingridients, value);
        }

        public Ingridient SelectedIngridient { get; set; }

        private User user { get; set; }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => this.RaiseAndSetIfChanged(ref _userName, value);
        }
        public MainWindowViewModel()
        { 
            RestaurantContext restaurantContext = new RestaurantContext();
            restaurantContext.Users.Load();
            restaurantContext.Ingridients.Load();
            Ingridients = restaurantContext.Ingridients.Local.ToObservableCollection();
            EditCommand = ReactiveCommand.Create(EditIngridient);
        }
        public ReactiveCommand<Unit,Unit> EditCommand { get; }
        public MainWindow Owner { get; set; }
        public void EditIngridient()
        {
            EditWindowIngridient editWindowIngridient = new EditWindowIngridient();
            editWindowIngridient.DataContext = new EditWindowIngridientViewModel(SelectedIngridient);
            editWindowIngridient.Show();
        }
        public MainWindowViewModel(User user) : this()
        { 
            this.user = user;
            _userName = $"Текущий пользователь: {user.Login} 😘😘😘";
        }
    }
}
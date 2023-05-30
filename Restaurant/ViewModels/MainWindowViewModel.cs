using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using Restaurant.Models;
using System.Collections.ObjectModel;
using System.Reactive;

namespace Restaurant.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<Ingridient> _ingridients;
        public ObservableCollection<Ingridient> Ingridients
        {
            get => _ingridients;
            set => this.RaiseAndSetIfChanged(ref _ingridients, value);
        }

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
        }
        public MainWindowViewModel(User user) : this()
        { 
            this.user = user;
            _userName = $"Текущий пользователь: {user.Login} 😘😘😘";
        }
    }
}
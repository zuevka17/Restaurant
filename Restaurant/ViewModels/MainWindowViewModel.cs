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
        RestaurantContext restaurantContext = new RestaurantContext();
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
            restaurantContext.Users.Load();
            restaurantContext.Ingridients.Load();
            Ingridients = restaurantContext.Ingridients.Local.ToObservableCollection();
            EditCommand = ReactiveCommand.Create(EditIngridient);
            AddCommand = ReactiveCommand.Create(AddIngridient);
            SaveCommand = ReactiveCommand.Create(SaveDbChabges);
            DelCommand = ReactiveCommand.Create(DeleteSelected);
        }
        public ReactiveCommand<Unit,Unit> EditCommand { get; set; }
        public ReactiveCommand<Unit,Unit> AddCommand { get; set; }
        public ReactiveCommand<Unit,Unit> SaveCommand { get; set; }
        public ReactiveCommand<Unit,Unit> DelCommand { get; set; }
        public MainWindow Owner { get; set; }
        public async void EditIngridient( )
        {
            EditWindowIngridient editWindowIngridient = new EditWindowIngridient();
            editWindowIngridient.DataContext = new EditWindowIngridientViewModel(SelectedIngridient);
            await editWindowIngridient.ShowDialog(Owner);
            var GoodsBack = Ingridients;
            Ingridients = null;
            Ingridients = GoodsBack;
           
        }
        public async void AddIngridient( )
        {
            restaurantContext.Ingridients.Add(new Ingridient());
            var lastItem = Ingridients.LastOrDefault();
            EditWindowIngridient editWindowIngridient = new EditWindowIngridient();
            editWindowIngridient.DataContext = new EditWindowIngridientViewModel(lastItem);
            await editWindowIngridient.ShowDialog(Owner);
            var GoodsBack = Ingridients;
            Ingridients = null;
            Ingridients = GoodsBack;
            restaurantContext.SaveChanges();
        }
        public void SaveDbChabges()
        {
            restaurantContext.SaveChanges();
        }
        public void DeleteSelected()
        {
            Ingridient ingridientForDelete = SelectedIngridient;
            restaurantContext.Entry(ingridientForDelete).State = EntityState.Deleted;
        }

        public MainWindowViewModel(User user, MainWindow owner) : this()
        { 
            this.user = user;
            _userName = $"Текущий пользователь: {user.Login} 😘😘😘";
            Owner = owner;
        }
    }
}
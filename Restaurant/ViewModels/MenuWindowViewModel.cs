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
    public class MenuWindowViewModel : ViewModelBase
    {
        RestaurantContext restaurantContext = new RestaurantContext();
        public ObservableCollection<Menu> _menus;
        public ObservableCollection<Menu> Menus
        {
            get => _menus;
            set => this.RaiseAndSetIfChanged(ref _menus, value);
        }

        public Menu SelectedMenu { get; set; }
        private User user { get; set; }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => this.RaiseAndSetIfChanged(ref _userName, value);
        }
        public MenuWindowViewModel()
        {
            restaurantContext.Users.Load();
            restaurantContext.Menus.Load();
            Menus = restaurantContext.Menus.Local.ToObservableCollection();
            EditCommand = ReactiveCommand.Create(EditMenu);
            AddCommand = ReactiveCommand.Create(AddMenu);
            SaveCommand = ReactiveCommand.Create(SaveDbChabges);
            DelCommand = ReactiveCommand.Create(DeleteSelected);
        }
        public ReactiveCommand<Unit, Unit> EditCommand { get; set; }
        public ReactiveCommand<Unit, Unit> AddCommand { get; set; }
        public ReactiveCommand<Unit, Unit> SaveCommand { get; set; }
        public ReactiveCommand<Unit, Unit> DelCommand { get; set; }
        public MenuWindow Owner { get; set; }
        public async void EditMenu()
        {
            EditWindowMenu editWindowMenu = new EditWindowMenu();
            editWindowMenu.DataContext = new EditWindowMenuViewModel(SelectedMenu);
            await editWindowMenu.ShowDialog(Owner);
            var GoodsBack = Menus;
            Menus = null;
            Menus = GoodsBack;

        }
        public async void AddMenu()
        {
            restaurantContext.Menus.Add(new Menu());
            var lastItem = Menus.LastOrDefault();
            EditWindowMenu editWindowIngridient = new EditWindowMenu();
            editWindowIngridient.DataContext = new EditWindowMenuViewModel(lastItem);
            await editWindowIngridient.ShowDialog(Owner);
            var GoodsBack = Menus;
            Menus = null;
            Menus = GoodsBack;
            restaurantContext.SaveChanges();
        }
        public void SaveDbChabges()
        {
            restaurantContext.SaveChanges();
        }
        public void DeleteSelected()
        {
            Menu ingridientForDelete = SelectedMenu;
            restaurantContext.Entry(ingridientForDelete).State = EntityState.Deleted;
        }

        public MenuWindowViewModel(User user, MenuWindow owner) : this()
        {
            this.user = user;
            _userName = $"Текущий пользователь: {user.Login} 😘😘😘";
            Owner = owner;
        }
    }
}
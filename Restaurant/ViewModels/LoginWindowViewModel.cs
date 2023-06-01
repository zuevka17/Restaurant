using ReactiveUI;
using Restaurant.Models;
using Restaurant.Views;
using Restaurant.ViewModels;
using System.Linq;
using System.Reactive;

namespace Restaurant.ViewModels
{
    public class LoginWindowViewModel : ViewModelBase
    {
        public string Login {get; set;} = string.Empty;
        public string Password { get; set;} = string.Empty;
        private string _message = string.Empty;
        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }
        public LoginWindow Owner { get; set; }
        public ReactiveCommand<Unit,Unit> AuthCommand { get; }
        public LoginWindowViewModel() { }
        public LoginWindowViewModel(LoginWindow _owner)
        {
            Owner = _owner;
            AuthCommand = ReactiveCommand.Create(Auth);
        }
        public void Auth()
        {
            RestaurantContext dbContext = new RestaurantContext();
            User? user = dbContext.Users.Where(u => u.Login == Login && u.Password == Password).FirstOrDefault();
            if (user == null)
            {
                Message = "Неверно введен логин или пароль!";
            }
            else
            {
                Message = string.Empty;
                MenuWindow menuWindow = new MenuWindow();
                menuWindow.DataContext = new MenuWindowViewModel(user, menuWindow);
                menuWindow.Show();
                Owner.Close();
            }
        }

    }
}

using Microsoft.CodeAnalysis.CSharp.Syntax;
using Restaurant.Models;

namespace Restaurant.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private User user { get; set; }
        public MainWindowViewModel() { }
        public MainWindowViewModel(User user) 
        { 
            this.user = user;
        }
    }
}
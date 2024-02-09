using AplicativoDesafioBTG.ViewModel;
using System.Text.RegularExpressions;

namespace AplicativoDesafioBTG.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }
    }

}

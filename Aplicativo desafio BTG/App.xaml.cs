namespace AplicativoDesafioBTG
{
    public partial class App : Application
    {
        public readonly static int alturaGrafico = 500;
        public readonly static int larguraGrafico = 600;
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            window.Width = 900;
            window.Height =680;

            return window;
        }
    }
}

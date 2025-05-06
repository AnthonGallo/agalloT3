namespace agalloT3;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Establecer tema claro
        UserAppTheme = AppTheme.Light;

        // Configurar la página principal con navegación
        MainPage = new NavigationPage(new Views.vUno())
        {
            BarBackgroundColor = Color.FromArgb("#5D4037"),
            BarTextColor = Colors.White
        };
    }
}
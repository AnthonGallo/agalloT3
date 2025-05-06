namespace agalloT3;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        UserAppTheme = AppTheme.Light;

        MainPage = new NavigationPage(new Views.vUno())
        {
            BarBackgroundColor = Color.FromArgb("#5D4037"),
            BarTextColor = Colors.White
        };
    }
}
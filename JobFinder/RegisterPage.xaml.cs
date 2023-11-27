using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using JobFinder.ViewModels;

namespace JobFinder;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
        BindingContext = new UserViewModel();
    }

    public void GoToLogin()
    {
        Navigation.PushAsync(new MainPage());
    }

    public async void Error(string errorMessage)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        ToastDuration duration = ToastDuration.Long;
        double fontSize = 14;
        var toast = Toast.Make(errorMessage, duration, fontSize);

        await toast.Show(cancellationTokenSource.Token);
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }
}
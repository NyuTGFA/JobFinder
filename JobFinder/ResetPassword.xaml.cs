using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using JobFinder.ViewModels;

namespace JobFinder;

public partial class ResetPassword : ContentPage
{
    public ResetPassword(UserViewModel userViewModel)
    {
        InitializeComponent();
        BindingContext = userViewModel;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Navigation.PopAsync();
    }

    public async void Error(string errorMessage)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        ToastDuration duration = ToastDuration.Long;
        double fontSize = 14;
        var toast = Toast.Make(errorMessage, duration, fontSize);

        await toast.Show(cancellationTokenSource.Token);
    }

    public async void GoToLogin()
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        ToastDuration duration = ToastDuration.Long;
        double fontSize = 14;
        var toast = Toast.Make("Password changed succesfully", duration, fontSize);

        await toast.Show(cancellationTokenSource.Token);
        await Navigation.PushAsync(new MainPage());
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }
}
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using JobFinder.ViewModels;

namespace JobFinder;

public partial class ForgotPassword : ContentPage
{
    UserViewModel userViewModel;
    public ForgotPassword()
    {
        InitializeComponent();
        userViewModel = new UserViewModel();
        BindingContext = userViewModel;
    }

    public async void RequestResetPassword(int code)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        ToastDuration duration = ToastDuration.Long;
        double fontSize = 14;
        var toast = Toast.Make($"Your reset password code is: {code}. Please do not share it with anyone", duration, fontSize);

        await toast.Show(cancellationTokenSource.Token);
        await Navigation.PushAsync(new ResetPassword(userViewModel));
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Navigation.PopAsync();
    }
}
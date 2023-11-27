using JobFinder.ViewModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace JobFinder
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new UserViewModel();
        }

        public void GoToFeed(string userId)
        {
            Navigation.PushAsync(new FeedPage(userId));
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
            Navigation.PushAsync(new ForgotPassword());
        }

        private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}
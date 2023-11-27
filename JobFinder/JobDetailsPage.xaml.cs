using JobFinder.ViewModels;

namespace JobFinder;

public partial class JobDetailsPage : ContentPage
{
    public JobDetailsPage(string jobId, string userId)
    {
        InitializeComponent();
        BindingContext = new JobDetailsViewModel(jobId, userId, this);
    }

    public void SetApplyButton(bool alreadyApplied)
    {
        if (alreadyApplied)
        {
            btnApply.IsEnabled = false;
            btnApply.Text = "Aplication sent";
            btnApply.TextColor = Color.FromRgb(0, 0, 0);
            btnApply.BackgroundColor = Color.FromHex("#E0E0E0");
        }
    }
}
using JobFinder.Models;
using JobFinder.ViewModels;

namespace JobFinder;

public partial class FeedPage : ContentPage
{
    private string _userId;
    public FeedPage(string userId)
    {
        InitializeComponent();
        BindingContext = new JobsViewModel();
        _userId = userId;
    }

    private void SeeDetails(object sender, EventArgs e)
    {
        if (sender is View view && view.BindingContext is Job job)
        {
            string jobId = job._id;
            Navigation.PushAsync(new JobDetailsPage(jobId, _userId));
        }
    }
}
using JobFinder.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JobFinder.ViewModels
{
    public class JobDetailsViewModel : INotifyPropertyChanged
    {
        HttpClient client;
        JsonSerializerOptions _serializerOptions;
        string baseURL = "http://www.empleo.somee.com/api/empleo";
        private string _userId;
        private string _jobId;
        private JobDetailsPage _JobDetailsPage;

        private Job _job;
        public Job Job
        {
            get { return _job; }
            set
            {
                if (_job != value)
                {
                    _job = value;
                    OnPropertyChanged(nameof(Job));
                }
            }
        }

        public JobDetailsViewModel(string jobId, string userId, JobDetailsPage jobDetailsPage)
        {
            client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            _jobId = jobId;
            _userId = userId;
            _JobDetailsPage = jobDetailsPage;
            GetJobDetails.Execute(null);
        }

        public ICommand GetJobDetails => new Command(async () =>
        {
            string url = baseURL;
            HttpResponseMessage response = await client.GetAsync($"{url}/id?id={_jobId}");
            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    JobsResponse data = await JsonSerializer.DeserializeAsync<JobsResponse>(responseStream, _serializerOptions);
                    Job = data.job;
                    OnPropertyChanged(nameof(Job));
                    _JobDetailsPage.SetApplyButton(data.alreadyApplied);
                }
            }
        });

        public ICommand ApplyToJob => new Command(async () =>
        {
            string url = baseURL;
            HttpResponseMessage response = await client.PostAsync($"{url}/apply/{_jobId}/{_userId}", null);
            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    _JobDetailsPage.SetApplyButton(true);
                }
            }
        });

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using JobFinder.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;
using System.Windows.Input;

namespace JobFinder.ViewModels
{
    class JobsViewModel : INotifyPropertyChanged
    {
        HttpClient client;
        JsonSerializerOptions _serializerOptions;
        string baseURL = "http://www.empleo.somee.com/api/empleo";
        public List<Job> Jobs { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set
            {
                if (_location != value)
                {
                    _location = value;
                    OnPropertyChanged(nameof(Location));
                }
            }
        }
        public JobsViewModel()
        {
            client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            GetAllJobsCommand.Execute(null);
        }

        public ICommand GetAllJobsCommand => new Command(async () =>
        {
            string url = baseURL;
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<Job>>(responseStream, _serializerOptions);
                    Jobs = data.OrderBy(d => d.createdAt).ToList();
                    OnPropertyChanged(nameof(Jobs));
                }
            }
        });

        public ICommand FilterJobs => new Command(async () =>
        {
            string url = baseURL;
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<Job>>(responseStream, _serializerOptions);
                    if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Location))
                    {
                        Jobs = data.Where(d => d.title.ToLower().Contains(Name.ToLower()) || d.description.ToLower().Contains(Name.ToLower()) || d.location.ToLower().Contains(Location.ToLower())).OrderBy(d => d.createdAt).ToList();
                    }

                    if (!string.IsNullOrEmpty(Name))
                    {
                        Jobs = data.Where(d => d.title.ToLower().Contains(Name.ToLower()) || d.description.ToLower().Contains(Name.ToLower())).OrderBy(d => d.createdAt).ToList();
                    }
                    else
                    {
                        Jobs = data.Where(d => d.location.ToLower().Contains(Location.ToLower())).OrderBy(d => d.createdAt).ToList();
                    }

                    OnPropertyChanged(nameof(Jobs));
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

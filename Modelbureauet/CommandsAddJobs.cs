using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Modelbureauet
{
    public class CommandsAddJobs : INotifyPropertyChanged
    {
        private DataAccessLayer _dal = new DataAccessLayer();

        private List<JobsDTO> _jobs = new List<JobsDTO>();
        public List<JobsDTO> Jobs
        {
            get { return _jobs; }
            set
            {
                _jobs = value;
                NotifyPropertyChanged("Jobs");
            }
        }

        public CommandsAddJobs()
        {
            LoadJobs();
        }

        public async void LoadJobs()
        {
            List<JobsDTO> _temp = await _dal.GetAllJobs();
            Jobs = _temp;
        }
    
        /*----- ADD JOBS WINDOW -----*/

        ICommand _addJobCommand;
        public ICommand AddJobCommand
        {
            get { return _addJobCommand ?? (_addJobCommand = new RelayCommand<object>(AddJob)); }
        }

        private async void AddJob(object parameter)
        {
            var values = (object[])parameter;

            string nameofCustomer = values[0].ToString();
            DateTime dateTime = System.Convert.ToDateTime(values[1]);
            int duration = System.Convert.ToInt32(values[2]);
            string location = values[3].ToString();
            int numberOfModels = System.Convert.ToInt32(values[4]);
            string comments = values[5].ToString();
            bool isPlanned = System.Convert.ToBoolean(values[6]);

            JobsDTO _job = new JobsDTO(nameofCustomer, dateTime, duration, location, numberOfModels, comments,
                isPlanned, 0);

            bool status = await _dal.AddJob(_job);

            if (status)
            {
                MessageBox.Show("Job is added.");
                LoadJobs();
            }
            else
            {
                MessageBox.Show("Job could not be added.");
            }
        }

        ICommand _updateJobCommand;
        public ICommand UpdateJobCommand
        {
            get { return _updateJobCommand ?? (_updateJobCommand = new RelayCommand<object>(UpdateJob)); }
        }

        private async void UpdateJob(object parameter)
        {
            var values = (object[])parameter;

            string nameofCustomer = values[0].ToString();
            DateTime dateTime = System.Convert.ToDateTime(values[1]);
            int duration = System.Convert.ToInt32(values[2]);
            string location = values[3].ToString();
            int numberOfModels = System.Convert.ToInt32(values[4]);
            string comments = values[5].ToString();
            bool isPlanned = System.Convert.ToBoolean(values[6]);
            long id = Convert.ToInt64(values[7]);

            JobsDTO _job = new JobsDTO(nameofCustomer, dateTime, duration, location, numberOfModels, comments,
                isPlanned, id);

            bool status = await _dal.UpdateJob(_job);

            if (status)
            {
                MessageBox.Show($"Job with customer name {nameofCustomer} is updated.");
                LoadJobs();
            }
            else
            {
                MessageBox.Show($"Job with customer name {nameofCustomer} could not be updated.");
            }
        }

        ICommand _deleteJobCommand;
        public ICommand DeleteJobCommand
        {
            get { return _deleteJobCommand ?? (_deleteJobCommand = new RelayCommand<object>(DeleteJob)); }
        }

        private async void DeleteJob(object parameter)
        {

            var values = (JobsDTO)parameter;

            bool status = await _dal.DeleteJob(values.Id);

            if (status)
            {
                MessageBox.Show("Job is removed.");
                LoadJobs();
            }
            else
            {
                MessageBox.Show("Job could not be removed.");
            }
        }


        #region INotifyPropertyChanged implementation

        public new event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}

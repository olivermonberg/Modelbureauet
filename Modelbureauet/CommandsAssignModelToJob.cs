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
    public class CommandsAssignModelToJob : INotifyPropertyChanged
    {
        private DataAccessLayer _dal = new DataAccessLayer();

        private List<JobsDTO> _assignedJobs = new List<JobsDTO>();
        public List<JobsDTO> AssignedJobs
        {
            get { return _assignedJobs; }
            set
            {
                _assignedJobs = value;
                NotifyPropertyChanged("AssignedJobs");
            }
        }

        public CommandsAssignModelToJob()
        {
            LoadAssignedJobs();
        }

        public async void LoadAssignedJobs()
        {
            List<JobsDTO> _temp = await _dal.GetAllAssignedJobs();
            AssignedJobs = _temp;
        }

        ICommand _assignModelToJobCommand;
        public ICommand AssignModelToJobCommand
        {
            get { return _assignModelToJobCommand ?? (_assignModelToJobCommand = new RelayCommand<object>(AssignModelToJob)); }
        }

        private async void AssignModelToJob(object parameter)
        {
            var values = (object[])parameter;

            ModelDTO model = (ModelDTO)values[0];
            JobsDTO jobs = (JobsDTO)values[1];

            bool status = await _dal.AddModelToJob(new ModelToJobAssignmentDTO(0, model.Id, jobs.Id));

            if (status)
            {
                MessageBox.Show($"Model named {model.Name} is assigned to the job with customer name {jobs.CustomerName}.");
                LoadAssignedJobs();
            }
            else
            {
                MessageBox.Show($"Model named { model.Name} could not be assigned to the job with customer name { jobs.CustomerName}.");
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

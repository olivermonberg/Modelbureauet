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
    public class Commands : INotifyPropertyChanged
    {
        private DataAccessLayer _dal = new DataAccessLayer();

        private List<ModelDTO> _models = new List<ModelDTO>();
        public List<ModelDTO> Models
        {
            get { return _models; }
            set
            {
                _models = value;
                NotifyPropertyChanged("Models");
            }
        }

        public Commands()
        {
            LoadModels();
        }

        public async void LoadModels()
        {
            List<ModelDTO> _temp = await _dal.GetAllModels();
            Models = _temp;
        }

        ICommand _addModelsBtnCommand;
        public ICommand AddModelsBtnCommand
        {
            get { return _addModelsBtnCommand ?? (_addModelsBtnCommand = new RelayCommand(AddModelsBtn)); }
        }

        private void AddModelsBtn()
        {
            AddModelWindow _addModelWindow = new AddModelWindow();
            _addModelWindow.Show();
        }

        ICommand _addJobsBtnCommand;
        public ICommand AddJobsBtnCommand
        {
            get { return _addJobsBtnCommand ?? (_addJobsBtnCommand = new RelayCommand(AddJobsBtn)); }
        }

        private void AddJobsBtn()
        {
            AddJobsWindow _addJobsWindow = new AddJobsWindow();
            _addJobsWindow.Show();
        }

        ICommand _manageJobsBtnCommand;
        public ICommand ManageJobsBtnCommand
        {
            get { return _manageJobsBtnCommand ?? (_manageJobsBtnCommand = new RelayCommand(ManageJobsBtn)); }
        }

        private void ManageJobsBtn()
        {
            ManageJobsWindow _manageJobsWindow = new ManageJobsWindow();
            _manageJobsWindow.Show();
        }

        /*----- ADD MODEL WINDOW -----*/

        ICommand _addModelCommand;
        public ICommand AddModelCommand
        {
            get { return _addModelCommand ?? (_addModelCommand = new RelayCommand<object>(AddModel)); }
        }

        private async void AddModel(object parameter)
        {
            var values = (object[])parameter;

            string name = values[0].ToString();
            int phoneNumber = System.Convert.ToInt32(values[1]);
            string address = values[2].ToString();
            int height = System.Convert.ToInt32(values[3]);
            int weight = System.Convert.ToInt32(values[4]);
            string hairColor = values[5].ToString();
            string comments = values[6].ToString();

            ModelDTO _model = new ModelDTO(name, phoneNumber, address, height, weight, hairColor, comments, 0);

            bool status = await _dal.AddModel(_model);

            if (status)
            {
                MessageBox.Show("Model is added.");
                LoadModels();
            }
            else
            {
                MessageBox.Show("Model could not be added.");
            }
        }

        ICommand _updateModelCommand;
        public ICommand UpdateModelCommand
        {
            get { return _updateModelCommand ?? (_updateModelCommand = new RelayCommand<object>(UpdateModel)); }
        }

        private async void UpdateModel(object parameter)
        {
            var values = (object[])parameter;

            string name = values[0].ToString();
            int phoneNumber = System.Convert.ToInt32(values[1]);
            string address = values[2].ToString();
            int height = System.Convert.ToInt32(values[3]);
            int weight = System.Convert.ToInt32(values[4]);
            string hairColor = values[5].ToString();
            string comments = values[6].ToString();
            long id = Convert.ToInt64(values[7]);

            ModelDTO _model = new ModelDTO(name, phoneNumber, address, height, weight, hairColor, comments, id);

            bool status = await _dal.UpdateModel(_model);

            if (status)
            {
                MessageBox.Show($"Model named {name} is updated.");
                LoadModels();
            }
            else
            {
                MessageBox.Show($"Model named {name} could not be updated.");
            }
        }

        ICommand _deleteModelCommand;
        public ICommand DeleteModelCommand
        {
            get { return _deleteModelCommand ?? (_deleteModelCommand = new RelayCommand<object>(DeleteModel)); }
        }

        private async void DeleteModel(object parameter)
        {
            
                var values = (ModelDTO)parameter;

                bool status = await _dal.DeleteModel(values.Id);

                if (status)
                {
                    MessageBox.Show("Model is removed.");
                    LoadModels();
                }
                else
                {
                    MessageBox.Show("Model could not be removed.");
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

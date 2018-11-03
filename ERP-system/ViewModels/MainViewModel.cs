using ERP_system.Entities;
using ERP_system.Infrastructure;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace ERP_system.ViewModels
{
    public class MainViewModel: ViewModel
    {
        public MainViewModel()
        {
            employeeController.LoadData();
            projectsController.LoadData();

            commandsDictionary.Add("save", () =>
            {
                InitNullFields(NewOrEditedEmployee);
                employeeController.Save(NewOrEditedEmployee);
                CollectionViewSource.GetDefaultView(EmployeesList).Refresh();
                OnPropertyChanged("SelectedEmployee");
                OnPropertyChanged("ChartValues");
                SelectedTabIndex = 0;
            });
            commandsDictionary.Add("edit", () =>
            {
                NewOrEditedEmployee = (Employee)employeeController.SelectedEmploye.Clone();
                SelectedTabIndex = 1;
            });
            commandsDictionary.Add("new", () =>
            {
                NewOrEditedEmployee = new Employee();
                SelectedTabIndex = 1;
            });
            commandsDictionary.Add("cancel", () =>
            {
                SelectedTabIndex = 0;
            });

        }
        public ObservableCollection<Employee> EmployeesList => employeeController.GetList();
        public Employee SelectedEmployee => employeeController.SelectedEmploye;
        public ObservableCollection<Project> ProjectsList => projectsController.GetList();
        public int EmployeeSelectedIndex
        {
            get
            {
                return employeeController.SelectedIndex;
            }
            set
            {
                employeeController.SelectedIndex = value;
                OnPropertyChanged("SelectedEmployee"); //for update information about Employee
                OnPropertyChanged("ChartLabels");
                OnPropertyChanged("ChartValues");
            }
        }

        private Employee newOrEditedEmployee;
        public Employee NewOrEditedEmployee
        {
            get
            {
                return newOrEditedEmployee;
            }
            set
            {
                newOrEditedEmployee = value;
                OnPropertyChanged("NewOrEditedEmployee");
            }
        }

        protected int selectedTabIndex = 0;
        public int SelectedTabIndex
        {
            get
            {
                return selectedTabIndex;
            }
            set
            {
                selectedTabIndex = value;
                OnPropertyChanged("SelectedTabIndex");
            }
        }

        private Dictionary<string, Action> commandsDictionary = new Dictionary<string, Action>();
        private DelegateCommand command;
        public DelegateCommand Command
        {
            get
            {
                return command ??
                  (command = new DelegateCommand(obj =>
                  {
                      commandsDictionary[obj as string]();
                  }));
            }
        }


        public string[] ChartLabels
        {
            get
            {
                return SelectedEmployee.Indicators.GetNames();
            }
        }
        public ChartValues<double> ChartValues
        {
            get
            {
                return SelectedEmployee.Indicators.GetValues();
            }
        }

    }
}

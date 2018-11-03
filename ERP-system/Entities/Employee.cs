using System;
using System.Windows.Media.Imaging;

namespace ERP_system.Entities
{
    public class Employee : IHaveId<Employee>, ICloneable
    {
        public string Id { get; set; }

        public string PicturePath { get; set; }
        public BitmapImage Image
        {
            get
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                if (PicturePath == "None" || PicturePath == null)
                {
                    bitmap.UriSource = new Uri(@"https://bizraise.pro/wp-content/uploads/2014/09/no-avatar-300x300.png",
                        UriKind.Absolute);
                }
                else
                {
                    bitmap.UriSource = new Uri(PicturePath, UriKind.Absolute);
                }
                bitmap.EndInit();


                return bitmap;
            }
        }

        public string Location { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime Dob { get; set; }
        public int Age { get; set; }

        public string PositionHeld { get; set; }
        public int Salary { get; set; }

        public int Rating{get;set;}

        public int AverageIndicatorsValue
        {
            get
            {
                return (indicators.CodeEfficiency + indicators.DrawingGraphics
                    + indicators.EfficiencyTeamwork + indicators.LeadershipSkills
                    + indicators.PercentSuccessfullyCompletedProjects) / 5;
            }
        }



        private EmployeeIndicators indicators = new EmployeeIndicators();
        public EmployeeIndicators Indicators
        {
            get { return indicators; }
        }

        public void UpdateItem(Employee item)
        {
            if (item == null) return;

            Age = item.Age;
            Dob = item.Dob;
            FirstName = item.FirstName;
            LastName = item.LastName;
            PicturePath = item.PicturePath;
            Location = item.Location;
            Salary = item.Salary;
            PositionHeld = item.PositionHeld;

            indicators.CodeEfficiency = item.indicators.CodeEfficiency;
            indicators.DrawingGraphics =  item.indicators.DrawingGraphics;
            indicators.EfficiencyTeamwork = item.indicators.EfficiencyTeamwork;
            indicators.LeadershipSkills = item.indicators.LeadershipSkills;
            indicators.PercentSuccessfullyCompletedProjects = item.indicators.PercentSuccessfullyCompletedProjects;
        }

        public object Clone()
        {
            return new Employee
            {
                Id = this.Id,
                Age = this.Age,
                Dob = this.Dob,
                Salary = this.Salary,
                PositionHeld = this.PositionHeld,
                FirstName = this.FirstName,
                LastName = this.LastName,
                PicturePath = this.PicturePath,
                Location = this.Location,
                indicators = (EmployeeIndicators)indicators.Clone()
            };
        }



        //показатели качества выполнения задач
        //рейтинг сотрудника среди всех сотруников фирмы

    }
}

using ERP_system.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;

namespace ERP_system.Controllers
{
    public class EmployeeController: ControllerBase<Employee>
    {
        private int _selectedIndex = 0;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
            }
        }
        public Employee SelectedEmploye => GetItemByIndex(_selectedIndex);

        /// <summary>
        /// Get employee data using the API
        /// </summary>
        public void LoadData()
        {
            WebRequest request = WebRequest.Create("https://randomuser.me/api/?nat=US&results=20");
            
            //I decided that it would be better to extract the necessary data through parse 
            //than to create structures for the full amount of data obtained.
            using (var response = request.GetResponse())
            {
                using (var dataStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(dataStream))
                    {
                        var obj = JObject.Parse(reader.ReadToEnd());
                        foreach (JToken user in obj["results"])
                        {
                            Employee employee = new Employee();
                            GenerateAddtionalInfo(employee);
                            foreach (JProperty property in user.Children())
                            {
                                switch (property.Name)
                                {
                                    case "id":
                                        employee.Id = property.Value["value"].ToString();
                                        break;
                                    case "name":
                                        employee.FirstName = property.Value["first"].ToString();
                                        employee.LastName = property.Value["last"].ToString();
                                        break;
                                    case "location":
                                        employee.Location = property.Value["city"].ToString();
                                        break;
                                    case "dob":
                                        int age;
                                        if(Int32.TryParse(property.Value["age"].ToString(), out age))
                                            employee.Age = age;
                                        DateTime dob;
                                        if(DateTime.TryParse(property.Value["date"].ToString(), out dob))
                                            employee.Dob = dob;
                                        break;
                                    case "picture":
                                        employee.PicturePath = property.Value["large"].ToString();
                                        break;

                                    default:
                                        break;
                                }
                            }
                            Add(employee);
                        }
                    }
                }
            }
            CalcEmployeesRating();
        }

        public void CalcEmployeesRating()
        {
            for (int i = 0; i < GetList().Count; i++)
            {
                int rating = 1;
                for (int j = 0; j < GetList().Count; j++)
                {
                    if (GetList()[i].AverageIndicatorsValue < GetList()[j].AverageIndicatorsValue && i != j)
                    {
                        rating++;
                    }
                }
                GetList()[i].Rating = rating;
            }
        }

        private void GenerateAddtionalInfo(Employee employee)
        {          
            employee.Salary = rnd.Next(0, 100000);
            switch (rnd.Next(0,4))
            {
                case 0:
                    employee.PositionHeld = "emloyee";
                    break;
                case 1:
                    employee.PositionHeld = "emloyee";
                    break;
                case 2:
                    employee.PositionHeld = "leader";
                    break;
                case 3:
                    employee.PositionHeld = "manager";
                    break;
                default:
                    break;
            }

            employee.Indicators.CodeEfficiency = rnd.Next(0, 101);
            employee.Indicators.DrawingGraphics = rnd.Next(0, 101);
            employee.Indicators.EfficiencyTeamwork = rnd.Next(0, 101);
            employee.Indicators.LeadershipSkills = rnd.Next(0, 101);
            employee.Indicators.PercentSuccessfullyCompletedProjects = rnd.Next(0, 101);
        }

    }
}

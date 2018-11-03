using ERP_system.Controllers;
using ERP_system.Entities;
using ERP_system.Infrastructure;
using System;
using System.Reflection;

namespace ERP_system.ViewModels
{
    public class ViewModel: ViewModelBase
    {
        protected EmployeeController employeeController = new EmployeeController();
        protected ProjectsController projectsController = new ProjectsController();

        public void InitNullFields<T>(T obj)
        {
            foreach (FieldInfo fieldInfo in obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (fieldInfo.GetValue(obj) == null)
                {
                    switch (fieldInfo.FieldType.Name)
                    {
                        case "String":
                            fieldInfo.SetValue(obj, "None");
                            break;
                        case "Int32":
                            fieldInfo.SetValue(obj, 0);

                            break;
                        case "DateTime":
                            fieldInfo.SetValue(obj, new DateTime());
                            break;

                        default:
                            Console.WriteLine("namespace ERP_system.ViewModels.ViewModel.InitNullFields"
                                + Environment.NewLine +
                                "Behavior not found for this type: " + fieldInfo.FieldType.Name);
                            break;
                    }
                }
                else if (fieldInfo.FieldType.Name == "EmployeeIndicators")
                {
                     InitNullFields((obj as Employee).Indicators);
                }
            }
        }
    }
}

using ERP_system.Entities;
using System;

namespace ERP_system.Controllers
{
    public class ProjectsController: ControllerBase<Project>
    {
        /// <summary>
        /// Generate random projects
        /// </summary>
        public void LoadData()
        {
            
            for (int i = 0; i < 5; i++)
            {
                Add(new Project()
                {
                    Id = i.ToString(),
                    Name = "Project " + (i + 1),
                    Client = "Client " + (i + 1),
                    Begin = new DateTime(),
                    End = new DateTime(),
                    PercentageOfCompletion = rnd.Next(0, 101)
                });
            }
        }
    }
}

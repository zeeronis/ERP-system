using System;

namespace ERP_system.Entities
{
    public class Project : IHaveId<Project>
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Client { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public double PercentageOfCompletion { get; set; }

        public void UpdateItem(Project item)
        {
            Name = item.Name;
            Client = item.Client;
            Begin = item.Begin;
            End = item.End;
            PercentageOfCompletion = item.PercentageOfCompletion;
        }
    }
}

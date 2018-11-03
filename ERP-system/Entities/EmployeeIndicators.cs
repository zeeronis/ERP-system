using LiveCharts;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ERP_system.Entities
{
    public class EmployeeIndicators: ICloneable
    {
        public int EfficiencyTeamwork { get; set; }
        public int CodeEfficiency { get; set; }
        public int DrawingGraphics { get; set; }
        public int LeadershipSkills { get; set; }
        public int PercentSuccessfullyCompletedProjects { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public string[] GetNames()
        {
            var fieldInfoArr = this.GetType().GetFields(
                BindingFlags.NonPublic | BindingFlags.Instance);

            string[] names = new string[fieldInfoArr.Length];
            for (int i = 0; i < fieldInfoArr.Length; i++)
                names[i] = SplitWords(fieldInfoArr[i].Name.Split('<')[1].Split('>')[0]);

            return names;
        }

        public ChartValues<double> GetValues()
        {
            var arr = this.GetType().GetFields(
               BindingFlags.NonPublic | BindingFlags.Instance);

            List<double> values = new List<double>();
            foreach (FieldInfo fieldInfo in arr)
                values.Add(Convert.ToDouble(fieldInfo.GetValue(this)));

            return new ChartValues<double>(values);
        }

        private string SplitWords(string str)
        {
            char[] charArray = str.ToCharArray();
            string result = charArray[0].ToString();
            int wordCounter = 0;
            for (int i = 1; i < charArray.Length; i++)
            {
                if (Char.IsUpper(charArray[i]))
                {
                    wordCounter++;
                    if (wordCounter == 2)
                    {
                        result += Environment.NewLine;
                        wordCounter = 0;
                    }
                    result += " " + Char.ToLower(charArray[i]);
                }
                else
                {
                    result += charArray[i];
                }
            }
            return result;
        }

        
    }
}

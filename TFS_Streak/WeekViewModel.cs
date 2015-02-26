using System.Collections.Generic;
namespace TFS_Streak
{
    public class WeekViewModel
    {
        private const int DaysInWeek = 7;

        public IList<int> Days { get; set; }

        public WeekViewModel()
        {
            Days = new List<int>(DaysInWeek);
        }

        public void AddEntry(int index, int val)
        {
            Days.Insert(index, val);
        }
    }
}
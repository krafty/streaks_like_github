using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFS_Streak
{
    public class StreakViewModel
    {
        private const int DaysInOneYear = 365;
        private const int DayInAWeek = 7;

        private IList<WeekViewModel> WeekViewModels { get; set; }
        public ICollectionView Weeks { get; private set; }
        DateTime _oneYearBack;
        private DateTime _today;

        public StreakViewModel(DateTime today)
        {
            _today = today;
            _oneYearBack = today - new TimeSpan(365, 0, 0, 0, 0);

            WeekViewModels = new List<WeekViewModel>();
            PopulateWeeks();
        }

        private void PopulateWeeks()
        {
            int numOfWeeks = DaysInOneYear / DayInAWeek;


        }
    }
}

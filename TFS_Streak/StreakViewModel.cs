using System;
using System.Collections.Generic;
using System.ComponentModel;
using TFS_Streak.Service;

namespace TFS_Streak
{
    public class StreakViewModel
    {
        private const int DayInAWeek = 7;
        private const int DaysInOneYear = 365;
        private DateTime _oneYearBack;

        private DateTime _today;
        private ISourceControlRepository _repo;

        public StreakViewModel(ISourceControlRepository repo, DateTime today)
        {
            _repo = repo;
            _today = today;
            _oneYearBack = today - new TimeSpan(365, 0, 0, 0, 0);

            WeekViewModels = new List<WeekViewModel>();
            PopulateWeeks();
        }

        public ICollectionView Weeks { get; private set; }

        private IList<WeekViewModel> WeekViewModels { get; set; }

        private void PopulateWeeks()
        {
            int numOfWeeks = DaysInOneYear / DayInAWeek;
            int dayOfWeek = (int)_oneYearBack.DayOfWeek;
            DateTime startDay = _oneYearBack - new TimeSpan(dayOfWeek, 0, 0, 0);

            for (int i = 0; i < numOfWeeks; i++)
            {
                WeekViewModel weekData = new WeekViewModel();
                for (int d = 0; d < DayInAWeek; d++)
                {
                    DateTime day = startDay + new TimeSpan((i * DayInAWeek + d), 0, 0, 0);
                    int numCheckins = _repo.GetNumberOfCommitsOnDay(day);
                    weekData.Days.Add(numCheckins);
                }

                WeekViewModels.Add(weekData); ;
            }
        }
    }
}
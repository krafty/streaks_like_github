using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using TfsStreak.Properties;
using TfsStreak.Service;

namespace TfsStreak
{
    public class StreakViewModel : ViewModelBase, IDisposable
    {
        #region Private Methods

        private bool CanGenerateStreaks(object arg)
        {
            return true;
        }

        private void LoadPreviousConfig()
        {
            Url = Settings.Default.Url;
            Password = Settings.Default.Password;
            UserName = Settings.Default.UserName;
            Path = Settings.Default.Path;
        }

        private void PopulateStreaksHandler(object obj)
        {
            WeekViewModels = new List<WeekViewModel>();
            PopulateWeeks();
        }

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
                    weekData.AddEntry(d, numCheckins);
                }

                WeekViewModels.Add(weekData);
            }

            Weeks = new ListCollectionView(WeekViewModels.ToList());
            RaisePropertyChanged("Weeks");
        }

        #endregion Private Methods

        #region Private Fields

        private const int DayInAWeek = 7;

        private const int DaysInOneYear = 365;

        private DateTime _oneYearBack;

        private ICommand _populateStreaks;

        private ISourceControlRepository _repo;

        private DateTime _today;

        #endregion Private Fields

        #region Public Constructors

        public StreakViewModel(ISourceControlRepository repo, DateTime today)
        {
            _repo = repo;
            _today = today;
            _oneYearBack = today - new TimeSpan(365, 0, 0, 0, 0);

            _populateStreaks = new InternalCommand(PopulateStreaksHandler, CanGenerateStreaks);
            LoadPreviousConfig();
        }

        #endregion Public Constructors

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion Public Events

        #region Public Properties

        public string Password
        {
            set
            {
                _repo.Password = value;
                RaisePropertyChanged("Password");
            }
        }

        public string Path
        {
            get
            {
                return _repo.Path;
            }
            set
            {
                _repo.Path = value;
                RaisePropertyChanged("Path");
            }
        }

        public ICommand PopulateStreaksCommand
        {
            get { return _populateStreaks; }
        }

        public string Url
        {
            get
            {
                return _repo.Url;
            }
            set
            {
                _repo.Url = value;
                RaisePropertyChanged("Url");
            }
        }

        public string UserName
        {
            get
            {
                return _repo.UserName;
            }
            set
            {
                _repo.UserName = value;
                RaisePropertyChanged("UserName");
            }
        }

        public ICollectionView Weeks { get; private set; }

        #endregion Public Properties

        #region Private Properties

        private IList<WeekViewModel> WeekViewModels { get; set; }

        #endregion Private Properties

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Properties.Settings.Default["Url"] = Url;
                Properties.Settings.Default["UserName"] = UserName;
                Properties.Settings.Default["Path"] = Path;
                Properties.Settings.Default.Save();
            }
        }

        #endregion Protected Methods
    }
}
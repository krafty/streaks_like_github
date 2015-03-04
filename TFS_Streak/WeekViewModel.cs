using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Linq;

namespace TfsStreak
{
    public class WeekViewModel : ViewModelBase
    {
        #region Private Fields

        private const int DaysInWeek = 7;

        #endregion Private Fields

        #region Public Constructors

        public WeekViewModel()
        {
            Days = new List<DayViewModel>(DaysInWeek);
        }

        #endregion Public Constructors

        #region Public Properties

        public IList<DayViewModel> Days { get; private set; }

        public ICollectionView DaysViewModel { get { return new ListCollectionView(Days.ToList()); } }

        #endregion Public Properties

        #region Public Methods

        public void AddEntry(int index, int value)
        {
            Days.Insert(index, new DayViewModel(value));
            RaisePropertyChanged("DaysViewModel");
        }

        #endregion Public Methods
    }
}
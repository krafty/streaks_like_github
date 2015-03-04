/*
 *    The code is written in C# using the free Visual Studio 2013 
 *    Community Edition and the TFS cloud - visualstudio.com in 
 *    my personal time at home.
 *    Copyright (C) 2015  Rishikesh Parkhe [rishiparkhe at outlook dot com]
 *
 *    This program is free software: you can redistribute it and/or modify
 *    it under the terms of the GNU General Public License as published by
 *    the Free Software Foundation, version 3 of the License.
 *
 *    This program is distributed in the hope that it will be useful,
 *    but WITHOUT ANY WARRANTY; without even the implied warranty of
 *    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *    GNU General Public License for more details.
 *
 *    You should have received a copy of the GNU General Public License
 *    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

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
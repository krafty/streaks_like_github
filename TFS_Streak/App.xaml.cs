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

using System;
using System.Windows;
using TfsStreak.Service;

namespace TfsStreak
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IDisposable
    {
        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _streakViewModel.Dispose();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Initialise();
        }

        #endregion Protected Methods

        #region Private Methods

        private void Initialise()
        {
            ISourceControlRepository repo = new TfsSourceControlRepository();
            _streakViewModel = new StreakViewModel(repo, DateTime.Now);
            MainWindow shell = new MainWindow(_streakViewModel);
            shell.Show();
        }

        #endregion Private Methods

        #region Private Fields

        private StreakViewModel _streakViewModel;

        #endregion Private Fields

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods
    }
}
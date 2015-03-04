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
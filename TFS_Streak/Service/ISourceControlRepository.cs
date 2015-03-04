using System;

namespace TfsStreak.Service
{
    public interface ISourceControlRepository
    {
        string Password { set; }

        string Path { get; set; }

        string Url { get; set; }

        string UserName { get; set; }

        #region Public Methods

        int GetNumberOfCommitsOnDay(DateTime day);

        #endregion Public Methods
    }
}
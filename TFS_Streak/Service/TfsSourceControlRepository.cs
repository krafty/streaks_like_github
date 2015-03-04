using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace TfsStreak.Service
{
    public class TfsSourceControlRepository : ISourceControlRepository
    {
        #region Private Fields

        private static Random _random = new Random(DateTime.Now.Second);
        private IEnumerable<Changeset> _queryResults = null;

        #endregion Private Fields

        #region Public Constructors

        public TfsSourceControlRepository()
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public string Password { private get; set; }

        public string Path { get; set; }

        public string Url { get; set; }

        public string UserName { get; set; }

        #endregion Public Properties

        #region Public Methods

        public int GetNumberOfCommitsOnDay(DateTime day)
        {
            if (_queryResults == null)
            {
                TfsTeamProjectCollection projects = new TfsTeamProjectCollection(
                  new Uri(Url), new NetworkCredential(UserName, Password));
                projects.EnsureAuthenticated();

                VersionControlServer sourceControl = projects.GetService<VersionControlServer>();
                _queryResults = sourceControl.QueryHistory(Path,
                    VersionSpec.Latest, 0, RecursionType.Full, "", null,
                    VersionSpec.Latest, Int32.MaxValue, true, false).Cast<Changeset>();
            }

            var results = _queryResults.Where(x => DatesMatches(x.CreationDate, day));
            return results.Count();
        }

        #endregion Public Methods

        #region Private Methods

        private static bool DatesMatches(DateTime first, DateTime second)
        {
            if (first.Day == second.Day && first.Month == second.Month && first.Year == second.Year)
                return true;
            else return false;
        }

        #endregion Private Methods

        //ItemSet items = vcServer.GetItems("$/*.", RecursionType.Full);
        //foreach (Item item in items.Items)
        //{
        //    Console.Write(item.ItemType.ToString());
        //    Console.Write(": ");
        //    Console.WriteLine(item.ServerItem.ToString());
        //}

        //return items.Items.Count();
    }
}
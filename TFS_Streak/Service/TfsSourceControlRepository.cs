using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TFS_Streak.Service
{
    public class TfsSourceControlRepository: ISourceControlRepository
    {
        public int GetNumberOfCommitsOnDay(DateTime day)
        {
            TfsTeamProjectCollection projects = new TfsTeamProjectCollection(
                  new Uri("https://rishikeshparkhe.visualstudio.com/DefaultCollection"), 
                      new NetworkCredential("rishiparkhe@outlook.com", "s1u2001g9"));
            projects.EnsureAuthenticated();

            VersionControlServer sourceControl = projects.GetService<VersionControlServer>();
            var results = sourceControl.QueryHistory(Path,
                VersionSpec.Latest, 0, RecursionType.Full, "", null, 
                VersionSpec.Latest, Int32.MaxValue, true, false).Cast<Changeset>();

            return results.Count();
        }

        //ItemSet items = vcServer.GetItems("$/*.", RecursionType.Full);
        //foreach (Item item in items.Items)
        //{
        //    Console.Write(item.ItemType.ToString());
        //    Console.Write(": ");
        //    Console.WriteLine(item.ServerItem.ToString());
        //}

        //return items.Items.Count();

        public string Path { get; set; }
    }
}

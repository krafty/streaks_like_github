using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFS_Streak.Service
{
    public interface ISourceControlRepository
    {
        int GetNumberOfCommitsOnDay(DateTime day);
    }
}

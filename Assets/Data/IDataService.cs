using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Data
{
    public interface IDataService
    {
        DataService Instance { get; }

        bool UpdateBalance(string viewerName, int amount);
        bool UpdateSubscriberTier(string viewerName, SubscriptionTierEnum newTier);
        bool UpdateBitsSpent(string viewerName, int amount);
        bool UpdateChannelPointsSpent(string viewerName, int amount);
        bool UpdateFollowerStatus(string viewerName, bool isFollower);
        bool NewViewer(string viewerName);
        bool GetBalance(string viewerName);
        bool GetSubscriberLevel(string viewerName);
        bool GetLastLogin(string viewerName);
    }
}

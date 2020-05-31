using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Data
{
    public class Viewer
    {
        public string ViewerId { get; set; }
        public string ViewerName { get; set; }
        public int Balance { get; set; }
        public SubscriptionTierEnum SubsriberLevel { get; set; }
        public bool IsFollower { get; set; }
        public int BitsSpent { get; set; }
        public int ChannelPointsSpent { get; set; }
        public DateTime? LastInteraction { get; set; }
    }
}

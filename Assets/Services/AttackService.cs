using Assets.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Services
{
    /// <summary>
    /// Manages the attacks for the varios blobs, ends the turn once the actions are done
    /// </summary>
    public class AttackService : IAttackService
    {
        private List<BaseAttack> _attacks;


        private static AttackService instance = null;
        private static readonly object padlock = new object();
        AttackService()
        {
        }

        public static AttackService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new AttackService();
                    }
                    return instance;
                }
            }
        }

        public void BeginAttacks(List<BaseAttack> attacks)
        {
            _attacks = attacks;
            foreach (var a in attacks)
            {
               
            }
        }



        public void CleanupAttack(int code)
        {
            _attacks.RemoveAt(code);
        }
    }
}

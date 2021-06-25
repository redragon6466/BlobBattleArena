using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Services.Interfaces
{
    public interface IAttackService
    {
        void BeginAttacks(List<BaseAttack> attacks);

        void CleanupAttack(int code);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMarioYeah.Entities;

namespace SuperMarioYeah.Enemies
{
    public interface IEnemy : IEntity
    {
        void TakeDamage();
    }
}

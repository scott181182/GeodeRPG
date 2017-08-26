using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarioYeah
{
    public static class ActionUtil
    {
        public static Action Chain(params Action[] cmds)
        {
            return () => { foreach (Action a in cmds) { a(); } };
        }
    }
}
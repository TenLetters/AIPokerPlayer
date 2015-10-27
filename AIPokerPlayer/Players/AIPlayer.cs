using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPokerPlayer.Players
{
    class AIPlayer : Player
    {
        public AIPlayer(string name, int startingChipCount): base(string name, int startingChipCount)
        { }

        public override void requestAction()
        {
            throw new NotImplementedException();
        }
    }
}

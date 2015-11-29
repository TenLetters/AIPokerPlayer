using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPokerPlayer.Players
{
    class AIPlayer : Player
    {
        public AIPlayer(string name, int startingChipCount, int position): base(name, startingChipCount, position)
        { }

        public override void requestAction()
        {
            throw new NotImplementedException();
        }

    }
}

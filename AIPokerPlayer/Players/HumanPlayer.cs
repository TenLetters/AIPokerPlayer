using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPokerPlayer.Players
{
    class HumanPlayer : Player
    {

        public HumanPlayer(string name, int startingChipCount, int position) : base(name, startingChipCount, position)
        { }
        
        public override void requestAction()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPokerPlayer.Poker.Moves
{
    class Call : Move
    {
        int callAmount;

        public Call(int amount)
        {
            this.callAmount = amount;
        }

        public int getCallAmount()
        {
            return callAmount;
        }

        public override String toString()
        {
            String s = "Call";
            return s;
        }
    }
}

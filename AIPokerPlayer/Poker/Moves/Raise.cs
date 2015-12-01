using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPokerPlayer.Poker.Moves
{
    class Raise : Move
    {
        int minimumRaise;
        int raiseAmount;

        public Raise(int minimumAmount)
        {
            if (minimumAmount > 0) //minimum raise is 1
            {
                this.minimumRaise = minimumAmount;
            }
            else
            {
                this.minimumRaise = 1;
            }
        }

        public int getMinimumRaise()
        {
            return minimumRaise;
        }

        public int getRaiseAmount()
        {
            return raiseAmount;
        }

        public void setRaiseAmount(int amount)
        {
            this.raiseAmount = amount;
        }

        public override String toString()
        {
            String s = "Raise";
            return s;
        }
    }
}

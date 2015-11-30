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
            this.minimumRaise = minimumAmount;
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
    }
}

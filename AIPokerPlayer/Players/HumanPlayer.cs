using AIPokerPlayer.Poker.Cards;
using AIPokerPlayer.Poker.Moves;
using AIPokerPlayer.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPokerPlayer.Players
{
    // Written by Alex Ciaramella
    class HumanPlayer : Player
    {
        public HumanPlayer(string name, int startingChipCount, int position) : base(name, startingChipCount, position)
        { }
        
        public override Move requestAction(List<Move> possibleMoves, List<Player> players)
        {
            while(getMoveChoice() == null)
            {
                //wait for user input
                //should we sleep a small bit?
            }
            return getMoveChoice();
        }
    }
}

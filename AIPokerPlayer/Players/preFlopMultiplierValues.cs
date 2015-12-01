using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace AIPokerPlayer
{
    /// <summary>
    /// this class holds the values the affect hands before the first card is revealed in the game
    /// the class is serializeable and is saved to the disk after every round
    /// </summary>
    [Serializable()]
    public class PreFlopMultiplierValues : ISerializable
    {
        double suitedMultiplier; 
        double pairMultiplier;
        double lowStraightChanceMultiplier;
        double highStraightChanceMultiplier;
        double highCardMultiplier; 
        double doubleHighCardMultiplier;
        double averageMultiplier;

        public PreFlopMultiplierValues()
        {
            //initial values only used when a learned file doesnt exist
            suitedMultiplier = 1.4;
            pairMultiplier = 2;
            lowStraightChanceMultiplier = 1.3;
            highStraightChanceMultiplier = 1.7;
            highCardMultiplier = 1.6;
            doubleHighCardMultiplier = 1.85;
            averageMultiplier = suitedMultiplier + pairMultiplier + lowStraightChanceMultiplier + highStraightChanceMultiplier + highCardMultiplier + doubleHighCardMultiplier;
        }

        //Deserialization constructor.
        public PreFlopMultiplierValues(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            suitedMultiplier = (double)info.GetValue("suitedMultiplier", typeof(double));
            pairMultiplier = (double)info.GetValue("pairMultiplier", typeof(double));
            lowStraightChanceMultiplier = (double)info.GetValue("lowStraightChanceMultiplier", typeof(double));
            highStraightChanceMultiplier = (double)info.GetValue("highStraightChanceMultiplier", typeof(double));
            highCardMultiplier = (double)info.GetValue("highCardMultiplier", typeof(double));
            doubleHighCardMultiplier = (double)info.GetValue("doubleHighCardMultiplier", typeof(double));
            averageMultiplier = (double)info.GetValue("averageMultiplier", typeof(double));
        }
        
        //Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("suitedMultiplier", suitedMultiplier);
            info.AddValue("pairMultiplier", pairMultiplier);
            info.AddValue("lowStraightChanceMultiplier", pairMultiplier);
            info.AddValue("highStraightChanceMultiplier", pairMultiplier);
            info.AddValue("highCardMultiplier", pairMultiplier);
            info.AddValue("doubleHighCardMultiplier", pairMultiplier);
            info.AddValue("averageMultiplier", averageMultiplier);
        }
    }
}

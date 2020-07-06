using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHandSorter
{
     class DeckOfCards : Card
    {
        const int NUM_OF_CARDS = 52; // total number of all cards
        private Card[] deck; // array of all cards that are being played

        public DeckOfCards()
        {
            deck = new Card[NUM_OF_CARDS];
        }

        public Card[] GetDeck { get { return deck; } }

        public void SetupDeck ()
        {
            int index = 0;
            foreach (SUIT s in Enum.GetValues (typeof(SUIT)))
            {
                foreach(VALUE v in Enum.GetValues(typeof(VALUE)))
                {
                    deck[index] = new Card { MySuit = s, MyValue = v };
                    index++;
                }
            }

            ShuffleCards();
        }
            
        public void ShuffleCards ()
        {
            Random rand = new Random();
            Card temp;

            for (int shuffleTimes = 0; shuffleTimes < 1000; shuffleTimes++)
            {
                for (int i = 0; i < NUM_OF_CARDS; i++)
                {
                    int secondCardIndex = rand.Next(13);
                    temp = deck[i];
                    deck[i] = deck[secondCardIndex];
                    deck[secondCardIndex] = temp;
                }
            }
        }
    }
}

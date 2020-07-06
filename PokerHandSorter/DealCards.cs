using System;
using System.Linq;

namespace PokerHandSorter
{
    class DealCards : DeckOfCards
    {
        private Card[] player1Hand;
        private Card[] player2Hand;
        private Card[] sortedPlayer1Hand;
        private Card[] sortedPlayer2Hand;

        public int TotalPlayer1Score = 0;
        public int TotalPlayer2Score = 0;

        public DealCards()
        {
            player1Hand = new Card[5];
            sortedPlayer1Hand = new Card[5];
            player2Hand = new Card[5];
            sortedPlayer2Hand = new Card[5];
        }

        public void Deal()
        {
            SetupDeck();
            ManipulateHands(ReadFile());
        }

        public void GetHands(string handsRow)
        {
            string[] allHands = handsRow.Split(' ');

            int index = 0;
            foreach (var hand in allHands)
            {
                if (index < 5)
                {
                    player1Hand[index] = new Card();
                    PreparePlayerHand(hand.Trim().ToUpper(), player1Hand[index]);
                }
                else if (index >= 5 && index < 10)
                {
                    player2Hand[index - 5] = new Card();
                    PreparePlayerHand(hand.Trim().ToUpper(), player2Hand[index - 5]);
                }
                index++;
            }
        }

        private void ManipulateHands(string[] rows)
        {
            foreach (var handsRow in rows)
            {
                GetHands(handsRow);
                SortCards();
                EvaluateHands();
            }
        }
        private string[] ReadFile()
        {
            try
            {
                TotalPlayer1Score = TotalPlayer2Score = 0;
                Console.Write("Enter filename with full path : ");
                string fileName = Console.ReadLine();

                string[] rows = System.IO.File.ReadAllLines(@fileName);
                return rows;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

        private void PreparePlayerHand(string hand, Card playerHand)
        {
            var value = hand.Substring(0, 1);
            var suit = hand.Substring(1, 1);

            switch (suit)
            {
                case "C":
                    playerHand.MySuit = SUIT.CLUBS;
                    break;
                case "D":
                    playerHand.MySuit = SUIT.DIAMONDS;
                    break;
                case "H":
                    playerHand.MySuit = SUIT.HEARTS;
                    break;
                case "S":
                    playerHand.MySuit = SUIT.SPADES;
                    break;
            }

            switch (value)
            {
                case "A":
                    playerHand.MyValue = VALUE.ACE;
                    break;
                case "8":
                    playerHand.MyValue = VALUE.EIGHT;
                    break;
                case "5":
                    playerHand.MyValue = VALUE.FIVE;
                    break;
                case "4":
                    playerHand.MyValue = VALUE.FOUR;
                    break;
                case "J":
                    playerHand.MyValue = VALUE.JACK;
                    break;
                case "K":
                    playerHand.MyValue = VALUE.KING;
                    break;
                case "9":
                    playerHand.MyValue = VALUE.NINE;
                    break;
                case "Q":
                    playerHand.MyValue = VALUE.QUEEN;
                    break;
                case "7":
                    playerHand.MyValue = VALUE.SEVEN;
                    break;
                case "6":
                    playerHand.MyValue = VALUE.SIX;
                    break;
                case "T":
                    playerHand.MyValue = VALUE.TEN;
                    break;
                case "3":
                    playerHand.MyValue = VALUE.THREE;
                    break;
                case "2":
                    playerHand.MyValue = VALUE.TWO;
                    break;
            }
        }

        public void SortCards()
        {
            var queryPlayer1 = from hand in player1Hand
                               orderby hand.MyValue
                               select hand;

            var queryPlayer2 = from hand in player2Hand
                               orderby hand.MyValue
                               select hand;

            var index = 0;

            foreach (var element in queryPlayer1.ToList())
            {
                sortedPlayer1Hand[index] = element;
                index++;
            }

            index = 0;

            foreach (var element in queryPlayer2.ToList())
            {
                sortedPlayer2Hand[index] = element;
                index++;
            }
        }

        public void DisplayCards()
        {
            Console.Clear();
            int x = 0;
            int y = 1;

            Console.WriteLine("PLAYER 1 HAND");

            for (int index = 0; index < 5; index++)
            {
                DrawCards.DrawCardOutline(x, y);
                DrawCards.DrawCardSuitValue(sortedPlayer1Hand[index], x, y);
                x++;
            }

            y = 15;
            x = 0;

            Console.SetCursorPosition(x, 14);
            Console.WriteLine("PLAYER 2 HAND");


            for (int index = 5; index < 10; index++)
            {
                DrawCards.DrawCardOutline(x, y);
                DrawCards.DrawCardSuitValue(sortedPlayer2Hand[index - 5], x, y);
                x++;
            }
        }       

        public void EvaluateHands()
        {
            HandEvaluator player1HandEvaluator = new HandEvaluator(sortedPlayer1Hand);
            HandEvaluator player2HandEvaluator = new HandEvaluator(sortedPlayer2Hand);

            Hand player1Hand = player1HandEvaluator.EvaluateHand();
            Hand player2Hand = player2HandEvaluator.EvaluateHand();

            if (player1Hand > player2Hand)
                TotalPlayer1Score += player1HandEvaluator.HandValues.Total;
            else if (player1Hand < player2Hand)
                TotalPlayer2Score += player2HandEvaluator.HandValues.Total;
            else if (player1HandEvaluator.HandValues.Total > player2HandEvaluator.HandValues.Total)
                TotalPlayer1Score += player1HandEvaluator.HandValues.Total;
            else if (player1HandEvaluator.HandValues.Total < player2HandEvaluator.HandValues.Total)
                TotalPlayer2Score += player2HandEvaluator.HandValues.Total;
            else if (player1HandEvaluator.HandValues.HighCard > player2HandEvaluator.HandValues.HighCard)
                TotalPlayer1Score += player1HandEvaluator.HandValues.Total;
            else if (player1HandEvaluator.HandValues.HighCard < player2HandEvaluator.HandValues.HighCard)
                TotalPlayer2Score += player2HandEvaluator.HandValues.Total;
        }
    }
}


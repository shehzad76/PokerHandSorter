using System;
using System.Text;

namespace PokerHandSorter
{
    class DrawCards
    {
        public static void DrawCardOutline(int xcoor, int ycoor)
        {
           //Console.ForegroundColor = ConsoleColor.White;

            int x = xcoor * 12;
            int y = ycoor;

            Console.SetCursorPosition(x, y);
            Console.Write(" __________\n");

            for (int index = 0; index < 10; index++)
            {
                Console.SetCursorPosition(x, y + 1 + index);

                if (index != 9)
                {
                    Console.WriteLine("|          |");
                }
                else
                {
                    Console.WriteLine("|__________|");
                }
            }
        }

        public static void DrawCardSuitValue(Card card, int xcoor, int ycoor)
        {
            char cardSuit = ' ';
            int x = xcoor * 12;
            int y = ycoor;

            switch (card.MySuit)
            {
                case Card.SUIT.HEARTS:
                    cardSuit = Encoding.GetEncoding(437).GetChars(new byte[] { 3 })[0];
                    //Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case Card.SUIT.DIAMONDS:
                    cardSuit = Encoding.GetEncoding(437).GetChars(new byte[] { 4 })[0];
                    //Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case Card.SUIT.CLUBS:
                    cardSuit = Encoding.GetEncoding(437).GetChars(new byte[] { 5 })[0];
                    //Console.ForegroundColor = ConsoleColor.Black;
                    break;

                case Card.SUIT.SPADES:
                    cardSuit = Encoding.GetEncoding(437).GetChars(new byte[] { 6 })[0];
                    //Console.ForegroundColor = ConsoleColor.Black;
                    break;
            }

            Console.SetCursorPosition(x + 5, y + 5);
            Console.Write(cardSuit);
            Console.SetCursorPosition(x + 4, y + 7);
            Console.Write(card.MyValue);

        }
    }
}
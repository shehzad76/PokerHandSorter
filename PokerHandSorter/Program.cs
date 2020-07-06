using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHandSorter
{
	public class Program
	{
		public static void Main()
		{
			Console.Title = "Poker Hand Sorter";
			DealCards dc = new DealCards();

			bool quit = false;
			while (!quit)
			{
				dc.Deal();

				Console.WriteLine("Player 1: {0}", dc.TotalPlayer1Score);
				Console.WriteLine("Player 2: {0}", dc.TotalPlayer2Score);

				char selection = ' ';
				while (!selection.Equals('Y') && !selection.Equals('N'))
				{
					Console.WriteLine("\n\nPlay again? Y/N");
					selection = Convert.ToChar(Console.ReadLine().ToUpper());

					if (selection.Equals('Y'))
						quit = false;
					else if (selection.Equals('N'))
						quit = true;
					else
						Console.WriteLine("Invalid Selection. Try again !");
				}
			}
		}

	}

}
	

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ConnectFourTests
{
	public class BotTests
	{
		[Test]
		public void WhenTheBoardIsEmptyTheBotMovesToTheFirstColumn()
		{
			const int firstColumn = 0;
			var emptyBoard = new List<Tuple<string, int>>();
			var nextMove = new Bot(firstColumn).Move(emptyBoard);
			Assert.That(nextMove, Is.EqualTo(firstColumn));
		}

		[TestCase(3)]
		[TestCase(4)]
		public void CompleteVerticalWinningLine(int column)
		{
			const int firstColumn = 0;
			var board = new List<Tuple<string, int>>
			{
				new Tuple<string, int>("yellow", column),
				new Tuple<string, int>("red", 1),
				new Tuple<string, int>("yellow", column),
				new Tuple<string, int>("red", 2),
				new Tuple<string, int>("yellow", column),
				new Tuple<string, int>("red", 4),
			};
			var nextMove = new Bot(firstColumn).Move(board);
			Assert.That(nextMove, Is.EqualTo(column));
		}
	}

	public class Bot
	{
		private readonly int _startingMove;
		private static int _winOpportunityLength = 3;

		public Bot(int startingMove)
		{
			_startingMove = startingMove;
		}

		public int Move(IEnumerable<Tuple<string, int>> board)
		{			
			for (int i = 0; i < 7; i++)
			{
				if (HasWinningVerticalLineInColumn(board, i))
				{
					return i;
				}	
			}
			return _startingMove;
		}

		private static bool HasWinningVerticalLineInColumn(IEnumerable<Tuple<string, int>> board, int column)
		{
			return board.Count(move => move.Item2 == column && move.Item1 == "yellow") == _winOpportunityLength;
		}
	}
}

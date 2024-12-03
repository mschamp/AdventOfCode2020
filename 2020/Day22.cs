using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace _2020
{
	public class Day22 : General.abstractPuzzleClass
    {
        public Day22():base(22, 2020) 
        {

        }
        public override string SolvePart1(string input = null)
        {
            string[] parts = input.Split(Environment.NewLine + Environment.NewLine);
            List<int> Player1Deck = LoadDeck(parts[0]);
            List<int> Player2Deck = LoadDeck(parts[1]);

            while (Player1Deck.Count>0&&Player2Deck.Count>0)
            {
                int p1 = Player1Deck[0];
                int p2 = Player2Deck[0];

                Player1Deck.RemoveAt(0);
                Player2Deck.RemoveAt(0);

                if (p1>p2)
                {
                    Player1Deck.Add(p1);
                    Player1Deck.Add(p2);
                }
                else
                {
                    Player2Deck.Add(p2);
                    Player2Deck.Add(p1);
                }
            }

            List<int> Winner;
            if (Player1Deck.Count>0)
            {
                Winner = Player1Deck;
            }
            else
            {
                Winner = Player2Deck;
            }

            return "" + CalculateScore(Winner);
        }

        private long CalculateScore(List<int> Deck)
        {
            int Result = 0;
            for (int i = 1; i <= (Deck.Count); i++)
            {
                Result += Deck[Deck.Count - i] * i;
            }
            return Result;
        }

        private long  game(List<int> Deck1, List<int> Deck2, int Game)
        {
            int Round = 0;
            HashSet<string> Decks = [];
            while (Deck1.Count > 0 && Deck2.Count > 0)
            {
                Round++;

                if (Decks.Contains(string.Join(",", Deck1) + (string.Join(";", Deck2))))
                {
                    return 1;
                }
                Decks.Add(string.Join(",", Deck1) + (string.Join(";", Deck2)));

                int p1 = Deck1[0];
                int p2 = Deck2[0];

                Deck1.RemoveAt(0);
                Deck2.RemoveAt(0);

                long Winner;

                if (Deck1.Count >= p1 && Deck2.Count >= p2)
                {
                    Winner = game(Deck1.Take(p1).ToList(), Deck2.Take(p2).ToList(),Game+1);
                }
                else
                {
                    if (p1 > p2)
                    {
                        Winner = 1;
                    }
                    else
                    {
                        Winner = 2;
                    }
                }

                switch (Winner)
                {
                    case 1:
                        Deck1.Add(p1);
                        Deck1.Add(p2);
                        break;
                    case 2:
                        Deck2.Add(p2);
                        Deck2.Add(p1);
                        break;
                }
            }

            if (Game==1)
            {
                if (Deck1.Count > 0)
                {
                    return CalculateScore(Deck1);
                }
                else
                {
                    return CalculateScore(Deck2);
                }
            }
            if (Deck1.Count > 0)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        
        private List<int> LoadDeck(string input)
        {
            List<int> PlayerDeck = [];
            foreach (string Card in input.Split(Environment.NewLine).Skip(1))
            {
                PlayerDeck.Add(int.Parse(Card));
            }
            return PlayerDeck;
        }

        public override string SolvePart2(string input = null)
        {
            string[] parts = input.Split(Environment.NewLine + Environment.NewLine);
            List<int> Player1Deck = LoadDeck(parts[0]);
            List<int> Player2Deck = LoadDeck(parts[1]);

            return "" + game(Player1Deck, Player2Deck, 1); ;
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"Player 1:
9
2
6
3
1

Player 2:
5
8
4
7
10") == "306");

            Debug.Assert(SolvePart2(@"Player 1:
9
2
6
3
1

Player 2:
5
8
4
7
10") == "291");
        }
    }
}

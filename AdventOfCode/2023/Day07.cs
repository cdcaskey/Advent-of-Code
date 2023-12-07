using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2023
{
    public class Day07(IInputLoader loader) : CodeChallenge(loader)
    {
        private readonly Dictionary<char, int> cardStrengths = new()
        {
            { '2', 2 },
            { '3', 3 },
            { '4', 4 },
            { '5', 5 },
            { '6', 6 },
            { '7', 7 },
            { '8', 8 },
            { '9', 9 },
            { 'T', 10 },
            { 'J', 11 },
            { 'Q', 12 },
            { 'K', 13 },
            { 'A', 14 },
        };

        public override object PartA()
        {
            var input = inputLoader.LoadArray<string>(InputLocation);
            var hands = ParseHands(input);
            hands = OrderHands(hands);

            var result = 0;
            for (var i = 0; i < hands.Count; i++)
            {
                result += hands[i].Bid * (i + 1);
            }

            return result;
        }

        public override object PartB() => throw new NotImplementedException();

        private List<Hand> ParseHands(string[] input)
        {
            var hands = new List<Hand>();
            for (var i = 0; i < input.Length; i++)
            {
                var parts = input[i].Split(' ');
                var cards = parts[0].ToCharArray();
                hands.Add(new(cards, CalculateHandType(cards), int.Parse(parts[1])));
            }

            return hands;
        }

        private HandType CalculateHandType(char[] cards)
        {
            var distinctCards = cards.Distinct().ToArray();

            switch(distinctCards.Length)
            {
                case 1:
                    return HandType.FiveOfAKind;

                case 2:
                    if (cards.Count(x => x == distinctCards[0]) == 4 || cards.Count(x => x == distinctCards[1]) == 4)
                    {
                        return HandType.FourOfAKind;
                    }
                    else
                    {
                        return HandType.FullHouse;
                    }

                case 3:
                    if (cards.Count(x => x == distinctCards[0]) == 3 ||
                        cards.Count(x => x == distinctCards[1]) == 3 ||
                        cards.Count(x => x == distinctCards[2]) == 3)
                    {
                        return HandType.ThreeOfAKind;
                    }
                    else
                    {
                        return HandType.TwoPair;
                    }

                case 4:
                    return HandType.OnePair;
            }

            return HandType.HighCard;
        }

        private List<Hand> OrderHands(List<Hand> hands)
            => hands.OrderBy(h => h.Type)
                    .ThenBy(h => cardStrengths[h.Cards[0]])
                    .ThenBy(h => cardStrengths[h.Cards[1]])
                    .ThenBy(h => cardStrengths[h.Cards[2]])
                    .ThenBy(h => cardStrengths[h.Cards[3]])
                    .ThenBy(h => cardStrengths[h.Cards[4]])
                    .ToList();

        private record Hand(char[] Cards, HandType Type, int Bid);

        private enum HandType
        {
            HighCard = 0,
            OnePair,
            TwoPair,
            ThreeOfAKind,
            FullHouse,
            FourOfAKind,
            FiveOfAKind
        }
    }
}

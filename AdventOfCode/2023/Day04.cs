using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2023
{
    public class Day04(IInputLoader loader) : CodeChallenge(loader)
    {
        public override object PartA()
        {
            var input = inputLoader.LoadArray<string>(InputLocation);
            var cards = ProcessCards(input);

            var result = 0;
            foreach (var card in cards)
            {
                result += CalculateCardScore(card);
            }

            return result;
        }

        public override object PartB()
        {
            var input = inputLoader.LoadArray<string>(InputLocation);
            var cards = ProcessCards(input);

            return CalculateWonCards(cards);
        }

        private static List<Card> ProcessCards(string[] input)
        {
            var cards = new List<Card>();

            var card = 1;
            foreach (var line in input)
            {
                var parts = line.Split(new char[] { ':', '|' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                var winningNumberStrings = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var winningNumbers = new List<int>();
                for (var i = 0; i < winningNumberStrings.Length; i++)
                {
                    winningNumbers.Add(int.Parse(winningNumberStrings[i]));
                }

                var numberStrings = parts[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var numbers =new List<int>();
                for (var i = 0; i < numberStrings.Length; i++)
                {
                    numbers.Add(int.Parse(numberStrings[i]));
                }

                cards.Add(new(card++, winningNumbers.ToArray(), numbers.ToArray()));
            }

            return cards;
        }

        private static int CalculateCardScore(Card card)
        {
            var winners = CalculateWinningNumbers(card);
            if (winners > 0)
            {
                var score = 1;
                for (var i = 1; i < winners; i++)
                {
                    score *= 2;
                }

                return score;
            }

            return 0;
        }

        private static int CalculateWonCards(List<Card> cards)
        {
            var wonCards = new List<Card>();
            wonCards.AddRange(cards);

            for(var i = 0; i < wonCards.Count; i++)
            {
                var winners = CalculateWinningNumbers(wonCards[i]);
                if (winners > 0)
                {
                    for (var j = 0; j < winners && wonCards[i].Id + j < cards.Count; j++)
                    {
                        wonCards.Add(cards[wonCards[i].Id + j]);
                    }
                }
            }

            return wonCards.Count;
        }

        private static int CalculateWinningNumbers(Card card)
        {
            var winners = 0;
            foreach (var number in card.Numbers)
            {
                if (card.WinningNumbers.Contains(number))
                {
                    winners++;
                }
            }

            return winners;
        }

        private class Card(int Id, int[] winningNumbers, int[] numbers)
        {
            public int Id { get; } = Id;
            public int[] WinningNumbers { get; } = winningNumbers;

            public int[] Numbers { get; } = numbers;

            public bool Consumed { get; set; }
        }
    }
}

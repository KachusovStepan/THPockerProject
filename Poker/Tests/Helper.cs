using System;
using System.Collections.Generic;
using GameLogic;

namespace Tests
{
    public static class Helper
    {
        static Dictionary<char, CardSuit> charSuits = new Dictionary<char, CardSuit>
        {
            { 'h', CardSuit.Hearts },
            { 'c', CardSuit.Clubs },
            { 's', CardSuit.Spades },
            { 'd', CardSuit.Diamonds }
        };

        static Dictionary<char, CardRank> charRanks = new Dictionary<char, CardRank>
        {
            { '2', CardRank.Two },
            { '3', CardRank.Three },
            { '4', CardRank.Four },
            { '5', CardRank.Five },
            { '6', CardRank.Six },
            { '7', CardRank.Seven },
            { '8', CardRank.Eight },
            { '9', CardRank.Nine },
            { '0', CardRank.Ten },
            { 'j', CardRank.Jack },
            { 'q', CardRank.Queen },
            { 'k', CardRank.King },
            { 'a', CardRank.Ace },
        };

        public static List<Card> CreateCards(string cardString)
        {
            var splitedCardString = cardString.Split(' ');
            var cards = new List<Card>();
            foreach (var card in splitedCardString)
            {
                cards.Add(new Card(charSuits[card[0]], charRanks[card[1]]));
            }

            return cards;
        }
    }
}

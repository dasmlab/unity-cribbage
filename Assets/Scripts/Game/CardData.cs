using System;
using UnityEngine;

namespace CribbageGame.Game
{
    [Serializable]
    public class Card
    {
        public string suit;
        public int rank;
        
        public Card(string suit, int rank)
        {
            this.suit = suit;
            this.rank = rank;
        }
        
        public string GetDisplayName()
        {
            string rankName = rank switch
            {
                1 => "Ace",
                11 => "Jack",
                12 => "Queen",
                13 => "King",
                _ => rank.ToString()
            };
            
            return $"{rankName} of {GetSuitName()}";
        }
        
        public string GetSuitName()
        {
            return suit switch
            {
                "C" => "Clubs",
                "D" => "Diamonds",
                "H" => "Hearts",
                "S" => "Spades",
                _ => suit
            };
        }
        
        public string GetSuitSymbol()
        {
            return suit switch
            {
                "C" => "♣",
                "D" => "♦",
                "H" => "♥",
                "S" => "♠",
                _ => suit
            };
        }
    }
} 
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CribbageGame.Game
{
    [Serializable]
    public class Player
    {
        public string id;
        public string name;
        public List<Card> hand;
        public int score;
        
        public Player(string id, string name)
        {
            this.id = id;
            this.name = name;
            this.hand = new List<Card>();
            this.score = 0;
        }
    }
    
    [Serializable]
    public class GameState
    {
        public List<Player> players;
        public List<Card> crib;
        public Card starter;
        public string state;
        public int turn;
        public int dealer;
        
        public GameState()
        {
            players = new List<Player>();
            crib = new List<Card>();
        }
    }
} 
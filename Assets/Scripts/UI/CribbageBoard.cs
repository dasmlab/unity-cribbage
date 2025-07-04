using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CribbageGame.Game;

namespace CribbageGame.UI
{
    public class CribbageBoard : MonoBehaviour
    {
        [Header("Board Layout")]
        [SerializeField] private Transform player1Track;
        [SerializeField] private Transform player2Track;
        [SerializeField] private GameObject pegPrefab;
        
        [Header("Scoring")]
        [SerializeField] private TextMeshProUGUI player1ScoreText;
        [SerializeField] private TextMeshProUGUI player2ScoreText;
        
        [Header("Animation")]
        [SerializeField] private float pegMoveSpeed = 2f;
        [SerializeField] private float pegMoveDelay = 0.1f;
        
        private List<GameObject> player1Pegs = new List<GameObject>();
        private List<GameObject> player2Pegs = new List<GameObject>();
        private int player1Score = 0;
        private int player2Score = 0;
        
        private void Start()
        {
            InitializeBoard();
        }
        
        private void InitializeBoard()
        {
            // Create initial pegs at position 0
            if (player1Track != null && pegPrefab != null)
            {
                GameObject peg1 = Instantiate(pegPrefab, player1Track);
                peg1.transform.localPosition = GetPegPosition(0);
                player1Pegs.Add(peg1);
            }
            
            if (player2Track != null && pegPrefab != null)
            {
                GameObject peg2 = Instantiate(pegPrefab, player2Track);
                peg2.transform.localPosition = GetPegPosition(0);
                player2Pegs.Add(peg2);
            }
            
            UpdateScoreTexts();
        }
        
        public void UpdateScores(GameState gameState)
        {
            if (gameState.players == null || gameState.players.Count < 2)
                return;
                
            int newPlayer1Score = gameState.players[0].score;
            int newPlayer2Score = gameState.players[1].score;
            
            // Animate score changes
            if (newPlayer1Score != player1Score)
            {
                StartCoroutine(AnimateScoreChange(0, player1Score, newPlayer1Score));
            }
            
            if (newPlayer2Score != player2Score)
            {
                StartCoroutine(AnimateScoreChange(1, player2Score, newPlayer2Score));
            }
            
            player1Score = newPlayer1Score;
            player2Score = newPlayer2Score;
        }
        
        private IEnumerator AnimateScoreChange(int playerIndex, int oldScore, int newScore)
        {
            Transform track = playerIndex == 0 ? player1Track : player2Track;
            List<GameObject> pegs = playerIndex == 0 ? player1Pegs : player2Pegs;
            
            // Create new peg if needed
            if (pegs.Count < 2)
            {
                GameObject newPeg = Instantiate(pegPrefab, track);
                newPeg.transform.localPosition = GetPegPosition(oldScore);
                pegs.Add(newPeg);
            }
            
            // Animate peg movement
            GameObject activePeg = pegs[pegs.Count - 1];
            Vector3 targetPosition = GetPegPosition(newScore);
            
            float elapsed = 0f;
            Vector3 startPosition = activePeg.transform.localPosition;
            
            while (elapsed < pegMoveSpeed)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / pegMoveSpeed;
                activePeg.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
                yield return null;
            }
            
            activePeg.transform.localPosition = targetPosition;
            
            // Check for win condition
            if (newScore >= 121)
            {
                Debug.Log($"Player {playerIndex + 1} wins!");
                // TODO: Trigger win animation/UI
            }
        }
        
        private Vector3 GetPegPosition(int score)
        {
            // Simple linear layout - you can make this more complex
            // with actual cribbage board hole positions
            float x = (score % 30) * 0.5f; // 30 points per row
            float y = (score / 30) * 0.5f; // New row every 30 points
            
            return new Vector3(x, y, 0);
        }
        
        private void UpdateScoreTexts()
        {
            if (player1ScoreText != null)
                player1ScoreText.text = $"Player 1: {player1Score}";
                
            if (player2ScoreText != null)
                player2ScoreText.text = $"Player 2: {player2Score}";
        }
        
        public void ResetBoard()
        {
            // Clear existing pegs
            foreach (GameObject peg in player1Pegs)
            {
                if (peg != null)
                    Destroy(peg);
            }
            player1Pegs.Clear();
            
            foreach (GameObject peg in player2Pegs)
            {
                if (peg != null)
                    Destroy(peg);
            }
            player2Pegs.Clear();
            
            // Reset scores
            player1Score = 0;
            player2Score = 0;
            
            // Reinitialize
            InitializeBoard();
        }
    }
} 
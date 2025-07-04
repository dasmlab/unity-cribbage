using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CribbageGame.Game;

namespace CribbageGame.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Game Controls")]
        [SerializeField] private Button dealButton;
        [SerializeField] private Button resetButton;
        
        [Header("Player Hands")]
        [SerializeField] private Transform remotePlayerHand;
        [SerializeField] private Transform currentPlayerHand;
        [SerializeField] private GameObject cardPrefab;
        
        [Header("Game Status")]
        [SerializeField] private TextMeshProUGUI statusText;
        [SerializeField] private TextMeshProUGUI turnText;
        [SerializeField] private TextMeshProUGUI dealerText;
        
        [Header("Error Display")]
        [SerializeField] private GameObject errorPanel;
        [SerializeField] private TextMeshProUGUI errorText;
        [SerializeField] private Button errorCloseButton;
        
        private List<GameObject> currentPlayerCards = new List<GameObject>();
        private List<GameObject> remotePlayerCards = new List<GameObject>();
        
        private void Start()
        {
            SetupButtons();
            SetupErrorPanel();
        }
        
        private void SetupButtons()
        {
            if (dealButton != null)
                dealButton.onClick.AddListener(() => GameManager.Instance.StartNewGame());
                
            if (resetButton != null)
                resetButton.onClick.AddListener(() => GameManager.Instance.ResetGame());
        }
        
        private void SetupErrorPanel()
        {
            if (errorPanel != null)
                errorPanel.SetActive(false);
                
            if (errorCloseButton != null)
                errorCloseButton.onClick.AddListener(() => errorPanel.SetActive(false));
        }
        
        public void UpdateGameState(GameState gameState)
        {
            UpdatePlayerHands(gameState);
            UpdateGameStatus(gameState);
        }
        
        private void UpdatePlayerHands(GameState gameState)
        {
            // Clear existing cards
            ClearPlayerCards();
            
            if (gameState.players == null || gameState.players.Count == 0)
                return;
                
            // Update current player's hand (bottom, cards visible)
            if (gameState.players.Count > 0)
            {
                DisplayPlayerHand(gameState.players[0], currentPlayerHand, currentPlayerCards, false);
            }
            
            // Update remote player's hand (top, cards face down)
            if (gameState.players.Count > 1)
            {
                DisplayPlayerHand(gameState.players[1], remotePlayerHand, remotePlayerCards, true);
            }
        }
        
        private void DisplayPlayerHand(Player player, Transform handContainer, List<GameObject> cardList, bool faceDown)
        {
            if (handContainer == null || cardPrefab == null)
                return;
                
            foreach (Card card in player.hand)
            {
                GameObject cardObj = Instantiate(cardPrefab, handContainer);
                CardDisplay cardDisplay = cardObj.GetComponent<CardDisplay>();
                
                if (cardDisplay != null)
                {
                    cardDisplay.SetCard(card, faceDown);
                }
                
                cardList.Add(cardObj);
            }
        }
        
        private void ClearPlayerCards()
        {
            foreach (GameObject card in currentPlayerCards)
            {
                if (card != null)
                    Destroy(card);
            }
            currentPlayerCards.Clear();
            
            foreach (GameObject card in remotePlayerCards)
            {
                if (card != null)
                    Destroy(card);
            }
            remotePlayerCards.Clear();
        }
        
        private void UpdateGameStatus(GameState gameState)
        {
            if (statusText != null)
                statusText.text = $"State: {gameState.state}";
                
            if (turnText != null)
                turnText.text = $"Turn: Player {gameState.turn + 1}";
                
            if (dealerText != null)
                dealerText.text = $"Dealer: Player {gameState.dealer + 1}";
        }
        
        public void ShowError(string errorMessage)
        {
            if (errorPanel != null && errorText != null)
            {
                errorText.text = errorMessage;
                errorPanel.SetActive(true);
            }
            
            Debug.LogError($"UI Error: {errorMessage}");
        }
    }
} 
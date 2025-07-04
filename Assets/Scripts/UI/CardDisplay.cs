using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CribbageGame.Game;

namespace CribbageGame.UI
{
    public class CardDisplay : MonoBehaviour
    {
        [Header("Card Visuals")]
        [SerializeField] private Image cardBackground;
        [SerializeField] private TextMeshProUGUI rankText;
        [SerializeField] private TextMeshProUGUI suitText;
        [SerializeField] private GameObject cardBack;
        
        [Header("Card Colors")]
        [SerializeField] private Color redColor = Color.red;
        [SerializeField] private Color blackColor = Color.black;
        
        private Card currentCard;
        private bool isFaceDown;
        
        public void SetCard(Card card, bool faceDown = false)
        {
            currentCard = card;
            isFaceDown = faceDown;
            UpdateVisuals();
        }
        
        private void UpdateVisuals()
        {
            if (currentCard == null)
                return;
                
            // Show/hide card back
            if (cardBack != null)
                cardBack.SetActive(isFaceDown);
                
            // Update card face
            if (!isFaceDown)
            {
                UpdateCardFace();
            }
        }
        
        private void UpdateCardFace()
        {
            if (currentCard == null)
                return;
                
            // Set rank text
            if (rankText != null)
            {
                string rankDisplay = currentCard.rank switch
                {
                    1 => "A",
                    11 => "J",
                    12 => "Q",
                    13 => "K",
                    _ => currentCard.rank.ToString()
                };
                rankText.text = rankDisplay;
            }
            
            // Set suit text and color
            if (suitText != null)
            {
                suitText.text = currentCard.GetSuitSymbol();
                
                // Set color based on suit
                Color textColor = (currentCard.suit == "H" || currentCard.suit == "D") ? redColor : blackColor;
                suitText.color = textColor;
                
                if (rankText != null)
                    rankText.color = textColor;
            }
        }
        
        public void FlipCard()
        {
            isFaceDown = !isFaceDown;
            UpdateVisuals();
        }
        
        public Card GetCard()
        {
            return currentCard;
        }
        
        public bool IsFaceDown()
        {
            return isFaceDown;
        }
    }
} 
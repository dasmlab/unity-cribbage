using System;
using System.Collections;
using UnityEngine;
using CribbageGame.Networking;
using CribbageGame.UI;

namespace CribbageGame.Game
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game Components")]
        [SerializeField] private GameServerAPI serverAPI;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private CribbageBoard cribbageBoard;
        
        [Header("Game Settings")]
        [SerializeField] private float statusUpdateInterval = 2f;
        
        public static GameManager Instance { get; private set; }
        public GameState CurrentGameState { get; private set; }
        public bool IsGameActive { get; private set; }
        
        private Coroutine statusUpdateCoroutine;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeGame();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void InitializeGame()
        {
            CurrentGameState = new GameState();
            IsGameActive = false;
            
            if (serverAPI == null)
                serverAPI = FindObjectOfType<GameServerAPI>();
                
            if (uiManager == null)
                uiManager = FindObjectOfType<UIManager>();
                
            if (cribbageBoard == null)
                cribbageBoard = FindObjectOfType<CribbageBoard>();
        }
        
        public void StartNewGame()
        {
            Debug.Log("Starting new game...");
            serverAPI.DealNewGame(
                OnGameDealt,
                OnGameError
            );
        }
        
        public void ResetGame()
        {
            Debug.Log("Resetting game...");
            serverAPI.ResetGame(
                OnGameReset,
                OnGameError
            );
        }
        
        private void OnGameDealt(GameState gameState)
        {
            Debug.Log("Game dealt successfully!");
            CurrentGameState = gameState;
            IsGameActive = true;
            
            // Update UI
            uiManager?.UpdateGameState(gameState);
            cribbageBoard?.UpdateScores(gameState);
            
            // Start status updates
            StartStatusUpdates();
        }
        
        private void OnGameReset()
        {
            Debug.Log("Game reset successfully!");
            CurrentGameState = new GameState();
            IsGameActive = false;
            
            // Update UI
            uiManager?.UpdateGameState(CurrentGameState);
            cribbageBoard?.ResetBoard();
            
            // Stop status updates
            StopStatusUpdates();
        }
        
        private void OnGameError(string error)
        {
            Debug.LogError($"Game error: {error}");
            uiManager?.ShowError(error);
        }
        
        private void StartStatusUpdates()
        {
            if (statusUpdateCoroutine != null)
                StopCoroutine(statusUpdateCoroutine);
                
            statusUpdateCoroutine = StartCoroutine(StatusUpdateLoop());
        }
        
        private void StopStatusUpdates()
        {
            if (statusUpdateCoroutine != null)
            {
                StopCoroutine(statusUpdateCoroutine);
                statusUpdateCoroutine = null;
            }
        }
        
        private IEnumerator StatusUpdateLoop()
        {
            while (IsGameActive)
            {
                yield return new WaitForSeconds(statusUpdateInterval);
                
                serverAPI.GetGameStatus(
                    OnStatusUpdated,
                    OnGameError
                );
            }
        }
        
        private void OnStatusUpdated(GameState gameState)
        {
            CurrentGameState = gameState;
            uiManager?.UpdateGameState(gameState);
            cribbageBoard?.UpdateScores(gameState);
        }
        
        private void OnDestroy()
        {
            StopStatusUpdates();
        }
    }
} 
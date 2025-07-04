using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using CribbageGame.Game;

namespace CribbageGame.Networking
{
    public class GameServerAPI : MonoBehaviour
    {
        [Header("Server Configuration")]
        [SerializeField] private string serverUrl = "http://localhost:8001";
        
        public static GameServerAPI Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void DealNewGame(Action<GameState> onSuccess, Action<string> onError)
        {
            StartCoroutine(DealCoroutine(onSuccess, onError));
        }
        
        public void GetGameStatus(Action<GameState> onSuccess, Action<string> onError)
        {
            StartCoroutine(StatusCoroutine(onSuccess, onError));
        }
        
        public void ResetGame(Action onSuccess, Action<string> onError)
        {
            StartCoroutine(ResetCoroutine(onSuccess, onError));
        }
        
        private IEnumerator DealCoroutine(Action<GameState> onSuccess, Action<string> onError)
        {
            string url = $"{serverUrl}/deal";
            
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                yield return request.SendWebRequest();
                
                if (request.result == UnityWebRequest.Result.Success)
                {
                    try
                    {
                        GameState gameState = JsonUtility.FromJson<GameState>(request.downloadHandler.text);
                        onSuccess?.Invoke(gameState);
                    }
                    catch (Exception e)
                    {
                        onError?.Invoke($"Failed to parse response: {e.Message}");
                    }
                }
                else
                {
                    onError?.Invoke($"Request failed: {request.error}");
                }
            }
        }
        
        private IEnumerator StatusCoroutine(Action<GameState> onSuccess, Action<string> onError)
        {
            string url = $"{serverUrl}/status";
            
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                yield return request.SendWebRequest();
                
                if (request.result == UnityWebRequest.Result.Success)
                {
                    try
                    {
                        GameState gameState = JsonUtility.FromJson<GameState>(request.downloadHandler.text);
                        onSuccess?.Invoke(gameState);
                    }
                    catch (Exception e)
                    {
                        onError?.Invoke($"Failed to parse response: {e.Message}");
                    }
                }
                else
                {
                    onError?.Invoke($"Request failed: {request.error}");
                }
            }
        }
        
        private IEnumerator ResetCoroutine(Action onSuccess, Action<string> onError)
        {
            string url = $"{serverUrl}/reset";
            
            using (UnityWebRequest request = UnityWebRequest.Post(url, ""))
            {
                yield return request.SendWebRequest();
                
                if (request.result == UnityWebRequest.Result.Success)
                {
                    onSuccess?.Invoke();
                }
                else
                {
                    onError?.Invoke($"Request failed: {request.error}");
                }
            }
        }
    }
} 
using System;
using InputManagement.Inputs;
using UnityEngine;
using Lucas.Currency;

namespace Lucas
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        [SerializeField] private InputManager inputManager;
        [SerializeField] private CurrencyManager currencyManager;
        
        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            Instance = this;
        }

        private void Start()
        {
            var coins = GameObject.FindObjectsOfType<Coin>();
            currencyManager.SubscribeToEvents(coins);
        }
    }
}

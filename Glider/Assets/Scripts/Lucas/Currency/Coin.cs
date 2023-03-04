using System;
using ReferenceVariables;
using UnityEngine;
using Lucas;

namespace Lucas.Currency
{
    public class CurrencyExchangeArgs : EventArgs
    {
        public float Value { get; private set; }

        public CurrencyExchangeArgs(float value)
        {
            Value = value;
        }
    }

    public class Coin : MonoBehaviour
    {
        [SerializeField] private CurrencyManager manager;
        [SerializeField] private FloatReference coinValue;

        public float CoinValue
        {
            get
            {
                return coinValue.Value;
            }
        }
        
        public delegate void OnCollection(object sender, CurrencyExchangeArgs e);
        public event OnCollection OnCollectionEvent;

        public void CallCollectionEvent()
        {
            var e = new CurrencyExchangeArgs(coinValue.Value);
            OnCollectionEvent?.Invoke(this, e);
        }
    }
}

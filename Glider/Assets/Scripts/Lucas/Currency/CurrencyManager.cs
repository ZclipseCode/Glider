using System;
using System.Collections.Generic;
using ReferenceVariables;
using UnityEngine;
namespace Lucas.Currency
{
    [CreateAssetMenu(fileName = "CurrencyManager", menuName = "Managers/Currency Manager")]
    public class CurrencyManager : ScriptableObject
    {
        [SerializeField] private FloatReference walletSize;
        [SerializeField] private EventChainObject currencyEventTarget;

        public void SubscribeToCollectionEvent(IEnumerable<Coin> coins)
        {
            foreach (var coin in coins)
            {
                coin.OnCollectionEvent += OnCoinCollectionEventCalled;
            }
        }

        private void OnCoinCollectionEventCalled(object sender, CurrencyExchangeArgs e)
        {
            if (sender is not Coin coin)
                return;
            UpdateCurrency(e.Value);
            Destroy(coin.gameObject);
        }

        private void UpdateCurrency(float value)
        {
            walletSize.Value += value;
            var e = new ChainedEventArgs(walletSize.Value);
            currencyEventTarget.CallEvent(this, e);
        }

        // Brian time
        public float GetWalletSize()
        {
            return walletSize.Value;
        }
        //
    }
}

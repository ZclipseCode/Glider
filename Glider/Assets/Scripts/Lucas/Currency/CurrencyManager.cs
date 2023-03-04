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

        public void SubscribeToEvents(IEnumerable<Coin> coins)
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
            walletSize.Value += coin.CoinValue;
            Destroy(coin.gameObject);
        }
    }
}

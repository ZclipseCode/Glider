using System;
using UnityEngine;
namespace Lucas.Currency
{
    [RequireComponent(typeof(Rigidbody))]
    public class CurrencyDetection : MonoBehaviour
    {   
        private void OnCollisionEnter(Collision collision)
        {
            var coin = collision.gameObject.GetComponent<Coin>();
            if (coin == null) return;
            coin.CallCollectionEvent();
        }
    }
}

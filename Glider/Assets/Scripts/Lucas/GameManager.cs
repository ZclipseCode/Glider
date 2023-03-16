using InputManagement.Inputs;
using UnityEngine;
using Lucas.Currency;
// Brian time
using System.IO;
//

namespace Lucas
{
    public class GameManager : MonoBehaviour
    {
        // This feels like an appropriate place for a singleton
        public static GameManager Instance;
        
        [SerializeField] private InputManager inputManager; // This is serialized because it needs to exist somewhere
                                                            // in a scene for it to use the OnEnable() event call
        [SerializeField] private CurrencyManager currencyManager;
        [SerializeField] private EventChainObject timerEventTarget;
        
        private int numCoins;
        private float timer;

        // Brian time
        bool once;
        //
        
        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            Instance = this;
        }

        private void Start()
        {
            var coins = GameObject.FindObjectsOfType<Coin>();
            numCoins = coins.Length;
            currencyManager.SubscribeToCollectionEvent(coins);
            SubscribeToCollectionEvent(coins);
        }

        private void Update()
        {
            if (numCoins > 0) UpdateTimer();
            else if (numCoins <= 0 && !once) // Brian time
            {
                CSVWriter writer = gameObject.AddComponent<CSVWriter>();
                writer.WritesCSV("Player", timer, currencyManager.GetWalletSize());
                Destroy(writer);

                CSVReader reader = gameObject.AddComponent<CSVReader>();
                reader.ReadCSV(Application.dataPath + "/GliderScores.csv");

                Destroy(reader);

                once = true;
            }
            //
        }

        private void SubscribeToCollectionEvent(Coin[] coins)
        {
            foreach (var coin in coins)
            {
                coin.OnCollectionEvent += OnCoinCollectionEventCalled;
            }
        }

        private void OnCoinCollectionEventCalled(object sender, CurrencyExchangeArgs e)
        {
            numCoins--;
        }
        
        private void UpdateTimer()
        {
            timer += Time.deltaTime;
            var e = new ChainedEventArgs(timer);
            timerEventTarget.CallEvent(this, e);
        }
    }
}

using System;
using TMPro;
using UnityEngine;
namespace Lucas
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UITextSetter : MonoBehaviour
    {
        [SerializeField] private EventChainObject eventSubscription;
        [SerializeField] private string defaultText;
        private TextMeshProUGUI uiTextElement;

        private void Awake()
        {
            uiTextElement = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            uiTextElement.text = defaultText;
            eventSubscription.ChainLinkEvent += OnChainLinkEventCalled;
        }

        private void OnChainLinkEventCalled(object sender, ChainedEventArgs e)
        {
            uiTextElement.text = Math.Round(e.Number, 2).ToString();
        }
    }
}

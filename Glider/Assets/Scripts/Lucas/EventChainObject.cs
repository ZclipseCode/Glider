using System;
using UnityEngine;
namespace Lucas
{
    public class ChainedEventArgs : EventArgs
    {
        public float Number { get; private set; }

        public ChainedEventArgs(float number)
        {
            Number = number;
        }
    }
    
    [CreateAssetMenu(fileName = "Event Chain Link")]
    public class EventChainObject : ScriptableObject
    {
        public delegate void ChainEvent(object sender, ChainedEventArgs e);

        public event ChainEvent ChainLinkEvent;

        public void CallEvent(object sender, ChainedEventArgs e)
        {
            ChainLinkEvent?.Invoke(sender, e);
        }
    }
}

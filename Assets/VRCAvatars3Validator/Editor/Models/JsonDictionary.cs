using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kogane
{
    [Serializable]
    public struct KeyValuePair
    {
        [SerializeField] private string key;

        [SerializeField] private string value;

        public string Key => key;
        public string Value => value;

        public KeyValuePair(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
    }

    [Serializable]
    public class JsonDictionary : ISerializationCallbackReceiver
    {
        public string this[string index] => dictionary.Where(x => x.Key == index).Single().Value;

        [SerializeField] private KeyValuePair[] dictionary = default;

        private Dictionary<string, string> m_dictionary;

        public Dictionary<string, string> Dictionary => m_dictionary;

        public JsonDictionary(Dictionary<string, string> dictionary)
        {
            m_dictionary = dictionary;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            dictionary = m_dictionary
                    .Select(x => new KeyValuePair(x.Key, x.Value))
                    .ToArray();
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            //m_dictionary = dictionary.ToDictionary(x => x.Key, x => x.Value);
            //dictionary = null;
        }
    }
}
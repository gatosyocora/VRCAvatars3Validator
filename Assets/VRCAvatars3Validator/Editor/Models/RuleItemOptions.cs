using System;
using UnityEngine;

namespace VRCAvatars3Validator.Models
{
    [Serializable]
    public class RuleItemOptions
    {
        [SerializeField]
        private string jsonText;

        public T ReadOptions<T>() where T : class
        {
            if (jsonText == null || jsonText == "")
                return null;
            return JsonUtility.FromJson<T>(jsonText);
        }

        public void WriteOptions(object options)
        {
            jsonText = JsonUtility.ToJson(options);
        }

        public void ChangeOptions<T>(ChangeOptionsValueDelegate<T> changeOptionsDelegate) where T : class
        {
            WriteOptions(changeOptionsDelegate(ReadOptions<T>()));
        }

        public void ChangeOptions<T>(ChangeOptionsVoidDelegate<T> changeOptionsDelegate) where T : class, new()
        {
            var options = ReadOptions<T>();
            if (options == null)
                options = new T();
            changeOptionsDelegate(options);
            WriteOptions(options);
        }

        public void ChangeOptions<T>(ChangeOptionsVoidDelegate<T> changeOptionsDelegate, ChangeOptionsIfNullDelegate<T> changeOptionsIfNullDelegate) where T : class
        {
            var options = ReadOptions<T>();
            if (options == null)
                options = changeOptionsIfNullDelegate();
            changeOptionsDelegate(options);
            WriteOptions(options);
        }

        public delegate T ChangeOptionsValueDelegate<T>(T options);
        public delegate void ChangeOptionsVoidDelegate<T>(T options);
        public delegate T ChangeOptionsIfNullDelegate<T>();
    }
}

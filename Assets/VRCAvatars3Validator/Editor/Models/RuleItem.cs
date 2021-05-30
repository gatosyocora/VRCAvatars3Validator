namespace VRCAvatars3Validator.Models
{
    [System.Serializable]
    public class RuleItem
    {
        public string Name;
        public string FilePath;
        public bool Enabled;
        public string Options;

        public T ReadOptions<T>() where T : class {
            if (Options == null || Options == "")
                return null;
            return UnityEngine.JsonUtility.FromJson<T>(Options);
        }

        public void WriteOptions(object options) {
            Options = UnityEngine.JsonUtility.ToJson(options);
        }

        public void ChangeOptions<T>(ChangeOptionsValueDelegate<T> changeOptionsDelegate) where T : class {
            WriteOptions(changeOptionsDelegate(ReadOptions<T>()));
        }

        public void ChangeOptions<T>(ChangeOptionsVoidDelegate<T> changeOptionsDelegate) where T : class, new() {
            var options = ReadOptions<T>();
            if (options == null)
                options = new T();
            changeOptionsDelegate(options);
            WriteOptions(options);
        }

        public void ChangeOptions<T>(ChangeOptionsVoidDelegate<T> changeOptionsDelegate, ChangeOptionsIfNullDelegate<T> changeOptionsIfNullDelegate) where T : class {
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

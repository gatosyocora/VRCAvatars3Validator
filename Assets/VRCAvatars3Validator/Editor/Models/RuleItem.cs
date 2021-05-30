namespace VRCAvatars3Validator.Models
{
    [System.Serializable]
    public class RuleItem
    {
        public string Name;
        public string FilePath;
        public bool Enabled;
        public string Options;

        public T LoadOptions<T>() where T : class {
            if (Options == null || Options == "")
                return null;
            return UnityEngine.JsonUtility.FromJson<T>(Options);
        }

        public void WriteOptions(object options) {
            Options = UnityEngine.JsonUtility.ToJson(options);
        }
    }
}

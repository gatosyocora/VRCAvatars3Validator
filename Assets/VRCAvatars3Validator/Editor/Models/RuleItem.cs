namespace VRCAvatars3Validator.Models
{
    [System.Serializable]
    public class RuleItem
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        public bool Enabled { get; set; }
    }
}
using UnityEngine;

namespace VRCAvatars3Validator.Models
{
    [CreateAssetMenu(menuName = "VRCAvatars3Validator/LanguagePack")]
    public class LanguagePack : ScriptableObject
    {
        [TextArea(100, 100)]
        public string data =
        "{\n" +
            "\"dictionary\":[\n" + 
            "{\"key\":\"hogehoge\",\"value\":\"ほげほげ\"},\n" +
            "]\n" +
        "}";
    }
}

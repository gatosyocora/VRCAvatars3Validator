using Kogane;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace VRCAvatars3Validator.Views
{
    public class LanguagePackAttribute : PropertyAttribute
    {
        public LanguagePackAttribute() { }
    }

    [CustomPropertyDrawer(typeof(LanguagePackAttribute))]
    public class LanguagePackDrawer : PropertyDrawer
    {
        public static int H = 18;
        private bool enable = false;

        public class Pair
        {
            public string key;
            public string value;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var y = position.y;
            var dictionary = Deserialize(property.stringValue);
            var keys = dictionary.Dictionary.Keys.Select(key => key).ToArray();
            enable = EditorGUI.Toggle(new Rect(position.x, y += H, position.width, position.height), enable);
            using (new EditorGUI.DisabledGroupScope(!enable))
            {
                for (int i = 0; i < keys.Length; i++)
                {
                    var pair = dictionary.Dictionary.Keys;
                    using (var check = new EditorGUI.ChangeCheckScope())
                    {
                        var key = keys[i];
                        var value = dictionary.Dictionary[key];
                        var newValue = EditorGUI.TextField(new Rect(position.x, y += H, position.width, position.height), key, value);

                        if (check.changed)
                        {
                            dictionary.Dictionary[key] = newValue;
                            property.stringValue = Serialize(dictionary);
                        }
                    }
                }
            }
        }

        private string Serialize(JsonDictionary dictionary)
        {
            return JsonUtility.ToJson(dictionary);
        }

        private JsonDictionary Deserialize(string data)
        {
            return JsonUtility.FromJson<JsonDictionary>(data);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
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

        public class Pair
        {
            public string key;
            public string value;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var y = position.y;
            var pairs = Deserialize(property.stringValue);
            for (int i = 0; i < pairs.Length; i++)
            {
                var pair = pairs[i];
                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    var newValue = EditorGUI.TextField(new Rect(position.x, y += H, position.width, position.height), pair.key, pair.value);

                    if (check.changed)
                    {
                        pairs[i].value = newValue;
                        property.stringValue = Serialize(pairs);
                    }
                }
            }
        }

        private string Serialize(Pair[] pairs)
        {
            var sb = new StringBuilder();
            sb.Append("{");
            sb.Append("\"dictionary\":[");
            sb.Append("{\"key\":\"hogehoge\",\"value\":\"hogehoge\"}");
            foreach (var pair in pairs)
            {
                sb.Append($",{{\"key\":\"{pair.key}\",\"value\":\"{pair.value}\"}}");
            }
            sb.Append("]");
            sb.Append("}");
            return sb.ToString();
        }

        private Pair[] Deserialize(string data)
        {
            var pairList = new List<Pair>();
            var matches = Regex.Matches(data, @",{\x22key\x22:\x22(?<key>(\w|\s|\.)+)\x22,\x22value\x22:\x22(?<value>(\w|\s|\.)+)\x22}");
            foreach (Match match in matches)
            {
                pairList.Add(new Pair
                {
                    key = match.Groups["key"].Value,
                    value = match.Groups["value"].Value
                });
            }
            return pairList.ToArray();
        }
    }
}


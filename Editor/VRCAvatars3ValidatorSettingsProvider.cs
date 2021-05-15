using System.Collections.Generic;
using UnityEditor;

namespace VRCAvatars3Validator
{
    public class VRCAvatars3ValidatorSettingsProvider : Editor
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new SettingsProvider("Project/VRCAvatars3Validator", SettingsScope.Project)
            {
                label = "VRCAvatars3Validator",
                keywords = new HashSet<string>(new string[] { "VRChat", "VRC", "Avatars3.0" }),
                guiHandler = (searchContext) =>
                {
                    var settings = ValidatorSettings.GetOrCreateSettings();
                    settings.validateOnUploadAvatar = EditorGUILayout.Toggle("Validate OnUploadAvatar", settings.validateOnUploadAvatar);

                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("Enable Rules", EditorStyles.boldLabel);

                    var ruleNames = new string[settings.validateRuleDictionary.Keys.Count];
                    settings.validateRuleDictionary.Keys.CopyTo(ruleNames, 0);

                    foreach (var ruleName in ruleNames)
                    {
                        var validateRule = settings.validateRuleDictionary[ruleName];
                        using (var check = new EditorGUI.ChangeCheckScope())
                        {
                            var valid = EditorGUILayout.ToggleLeft(ruleName, validateRule);

                            if (check.changed)
                            {
                                settings.validateRuleDictionary[ruleName] = valid;
                            }
                        }
                    }
                }
            };
        }
    }
}
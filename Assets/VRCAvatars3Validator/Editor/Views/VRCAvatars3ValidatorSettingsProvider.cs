using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace VRCAvatars3Validator.Views
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
                    var settings = ValidatorSettingsService.GetOrCreateSettings();
                    settings.validateOnUploadAvatar = EditorGUILayout.Toggle("Validate OnUploadAvatar", settings.validateOnUploadAvatar);

                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("Enable Rules", EditorStyles.boldLabel);

                    var ruleNames = settings.rules.Select(rule => rule.Name).ToArray();

                    for (int i = 0; i < ruleNames.Length; i++)
                    {
                        var validateRule = settings.rules[i].Enabled;
                        var ruleName = ruleNames[i];
                        var ruleSummary = RuleManager.FilePath2IRule(settings.rules[i].FilePath).RuleSummary;
                        using (var check = new EditorGUI.ChangeCheckScope())
                        {
                            settings.rules[i].Enabled = EditorGUILayout.ToggleLeft($"[{ruleName}] {ruleSummary}", validateRule);
                        }
                    }
                }
            };
        }
    }
}
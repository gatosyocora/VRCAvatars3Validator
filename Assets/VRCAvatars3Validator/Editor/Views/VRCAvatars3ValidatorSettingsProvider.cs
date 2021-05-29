using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using VRCAvatars3Validator.Utilities;

namespace VRCAvatars3Validator.Views
{
    public class VRCAvatars3ValidatorSettingsProvider : Editor
    {
#if VRC_SDK_VRCSDK3
        [SettingsProvider]
#endif
        public static SettingsProvider CreateSettingsProvider()
        {
            return new SettingsProvider("Project/VRCAvatars3Validator", SettingsScope.Project)
            {
                label = "VRCAvatars3Validator",
                keywords = new HashSet<string>(new string[] { "VRChat", "VRC", "Avatars3.0" }),
                guiHandler = (searchContext) =>
                {
                    var settings = ValidatorSettingsUtility.GetOrCreateSettings();
                    settings.validateOnUploadAvatar = EditorGUILayout.Toggle("Validate OnUploadAvatar", settings.validateOnUploadAvatar);

                    EditorGUILayout.Space();

                    settings.suspendUploadingByWarningMessage = EditorGUILayout.ToggleLeft("Suspend uploading by warning message", settings.suspendUploadingByWarningMessage);

                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("Enable Rules", EditorStyles.boldLabel);

                    var ruleNames = settings.rules.Select(rule => rule.Name).ToArray();

                    for (int i = 0; i < ruleNames.Length; i++)
                    {
                        var validateRule = settings.rules[i].Enabled;
                        var ruleName = ruleNames[i];
                        var ruleSummary = RuleUtility.FilePath2IRule(settings.rules[i].FilePath).RuleSummary;
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
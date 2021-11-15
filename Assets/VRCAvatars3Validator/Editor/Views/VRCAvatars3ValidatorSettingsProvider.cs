using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRCAvatars3Validator.Models;
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
                    settings.validateOnUploadAvatar = EditorGUILayout.Toggle(Localize.Translate("ValidateOnUploadAvatar"), settings.validateOnUploadAvatar);

                    EditorGUILayout.Space();

                    settings.suspendUploadingByWarningMessage = EditorGUILayout.ToggleLeft(Localize.Translate("SuspendWarning"), settings.suspendUploadingByWarningMessage);

                    EditorGUILayout.Space();

                    settings.languageType = (LanguageType)EditorGUILayout.EnumPopup(Localize.Translate("language"), settings.languageType);

                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField(Localize.Translate("EnableRules"), EditorStyles.boldLabel);

                    var ruleNames = settings.rules.Select(rule => rule.Name).ToArray();
                    var rules = settings.rules.Select(x => RuleUtility.FilePath2IRule(x.FilePath)).ToArray();

                    for (int i = 0; i < ruleNames.Length; i++)
                    {
                        var validateRule = settings.rules[i].Enabled;
                        var ruleName = ruleNames[i];
                        var ruleSummary = rules[i].RuleSummary;
                        settings.rules[i].Enabled = EditorGUILayout.ToggleLeft($"[{ruleName}] {ruleSummary}", validateRule);
                    }

                    EditorGUILayout.Space();

                    for (int i = 0; i < rules.Length; i++)
                    {
                        var rule = rules[i];
                        if (rule is Settingable settingableRule)
                        {
                            EditorGUILayout.LabelField(rule.GetType().Name, EditorStyles.boldLabel);
                            settingableRule.OnGUI(settings.rules[i].Options);
                        }
                        EditorGUILayout.Space();
                    }

                    using (new EditorGUILayout.HorizontalScope())
                    {
                        GUILayout.FlexibleSpace();

                        if (GUILayout.Button(Localize.Translate("Save")))
                        {
                            EditorUtility.SetDirty(settings);
                            AssetDatabase.SaveAssets();
                            AssetDatabase.Refresh();
                        }
                    }
                }
            };
        }
    }
}
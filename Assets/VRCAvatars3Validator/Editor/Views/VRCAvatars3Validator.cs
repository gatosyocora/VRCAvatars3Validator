using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace VRCAvatars3Validator
{
    public sealed class VRCAvatars3Validator : EditorWindow
    {
        private VRCAvatarDescriptor avatar;

        private Dictionary<int, IEnumerable<ValidateResult>> resultDictionary;

        private Vector2 scrollPos = Vector2.zero;

        private ValidatorSettings _settings;

        [MenuItem("VRCAvatars3Validator/Editor")]
        public static void Open()
        {
            GetWindow<VRCAvatars3Validator>(nameof(VRCAvatars3Validator));
        }

        private void OnOpen()
        {
            if (_settings == null)
            {
                _settings = ValidatorSettingsService.GetOrCreateSettings();
            }

            if (!avatar && Selection.activeGameObject)
            {
                avatar = Selection.activeGameObject.GetComponent<VRCAvatarDescriptor>();
                resultDictionary = ValidateAvatars3(avatar, _settings.rules);
            }
        }

        public void OnGUI()
        {
            OnOpen();

            EditorGUILayout.Space();

            avatar = EditorGUILayout.ObjectField("Avatar", avatar, typeof(VRCAvatarDescriptor), true) as VRCAvatarDescriptor;

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Rules", EditorStyles.boldLabel);

            using (new EditorGUI.IndentLevelScope())
            {
                for (int i = 0; i < _settings.rules.Count; i++)
                {
                    var rule = RuleManager.FilePath2IRule(_settings.rules[i].FilePath);
                    _settings.rules[i].Enabled = EditorGUILayout.ToggleLeft(
                                                            $"[{i + 1}] {rule.RuleSummary}",
                                                            _settings.rules[i].Enabled);
                }
            }

            EditorGUILayout.Space();

            using (new EditorGUI.DisabledGroupScope(avatar is null))
            {
                if (GUILayout.Button("Validate"))
                {
                    resultDictionary = ValidateAvatars3(avatar, _settings.rules);
                }
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Errors", EditorStyles.boldLabel);

            if (resultDictionary is null) return;

            if (resultDictionary.Any())
            {
                using (var scroll = new EditorGUILayout.ScrollViewScope(scrollPos))
                {
                    scrollPos = scroll.scrollPosition;
                    foreach (var resultPair in resultDictionary)
                    {
                        var ruleId = resultPair.Key;
                        var results = resultPair.Value;

                        foreach (var result in results)
                        {
                            DrawResultItem(ruleId, result);
                        }
                    }
                }
            }
            else
            {
                EditorGUILayout.HelpBox("No Error", MessageType.Info);
            }

        }

        private void DrawResultItem(int ruleId, ValidateResult result)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.HelpBox($"[{ruleId}] {result.ResultMessage}",
                    result.ResultType == ValidateResult.ValidateResultType.Error ? MessageType.Error :
                    result.ResultType == ValidateResult.ValidateResultType.Warning ? MessageType.Warning : MessageType.None);

                using (new EditorGUILayout.VerticalScope(GUILayout.Width(60f)))
                {
                    if (GUILayout.Button("Select", GUILayout.Width(60f)))
                    {
                        FocusTarget(result);
                    }

                    using (new EditorGUI.DisabledGroupScope(!result.CanAutoFix))
                    {
                        if (GUILayout.Button("AutoFix", GUILayout.Width(60f)))
                        {
                            result.AutoFix();
                        }
                    }
                }
            }
        }

        public static Dictionary<int, IEnumerable<ValidateResult>> ValidateAvatars3(VRCAvatarDescriptor avatar, IEnumerable<RuleItem> rules)
        {
            return rules.Select((rule, index) => new { Rule = rule, Index = index})
                .Where(rulePair => rulePair.Rule.Enabled)
                .Select(rulePair =>
                {
                    var rule = RuleManager.FilePath2IRule(rulePair.Rule.FilePath);
                    var results = rule.Validate(avatar);
                    return new KeyValuePair<int, IEnumerable<ValidateResult>>(rulePair.Index + 1, results);
                })
                .ToDictionary(resultPair => resultPair.Key, resultPair => resultPair.Value);
        }

        public void FocusTarget(ValidateResult result)
        {
            Selection.activeObject = result.Target;
            EditorGUIUtility.PingObject(result.Target);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDKBase.Editor.BuildPipeline;

namespace VRCAvatars3Validator
{
    public sealed class VRCAvatars3Validator : EditorWindow, IVRCSDKBuildRequestedCallback
    {
        private VRCAvatarDescriptor avatar;

        private Dictionary<int, IEnumerable<ValidateResult>> resultDictionary;

        private Vector2 scrollPos = Vector2.zero;

        private readonly Dictionary<int, RuleItem> ruleDictionary = new Dictionary<int, RuleItem>();

        private ValidatorSettings validatorSettings;

        public int callbackOrder => -1;

        private class RuleItem
        {
            public IRule Rule { get; set; }
            public bool Enabled { get; set; }
        }

        [MenuItem("VRCAvatars3Validator/Editor")]
        public static void Open()
        {
            GetWindow<VRCAvatars3Validator>(nameof(VRCAvatars3Validator));
        }

        public void OnEnable()
        {
            var rules = RuleManager.GetRules().ToArray();

            for (int i = 0; i < rules.Length; i++)
            {
                ruleDictionary.Add(i + 1, new RuleItem
                {
                    Enabled = true,
                    Rule = rules[i]
                });
            }
            FetchRuleValidateEnabled();
        }

        private void OnOpen()
        {
            if (!avatar && Selection.activeGameObject)
            {
                avatar = Selection.activeGameObject.GetComponent<VRCAvatarDescriptor>();
                resultDictionary = ValidateAvatars3(avatar, ruleDictionary);
            }

            FetchRuleValidateEnabled();
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
                foreach (var ruleItem in ruleDictionary)
                {
                    ruleItem.Value.Enabled = EditorGUILayout.ToggleLeft($"[{ruleItem.Key}] {ruleItem.Value.Rule.RuleSummary}", ruleItem.Value.Enabled);
                }
            }

            EditorGUILayout.Space();

            using (new EditorGUI.DisabledGroupScope(avatar is null))
            {
                if (GUILayout.Button("Validate"))
                {
                    resultDictionary = ValidateAvatars3(avatar, ruleDictionary.Where(ruleItem => ruleItem.Value.Enabled));
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

        private Dictionary<int, IEnumerable<ValidateResult>> ValidateAvatars3(VRCAvatarDescriptor avatar, IEnumerable<KeyValuePair<int, RuleItem>> ruleDictionary)
        {
            return ruleDictionary.Select(rulePair =>
            {
                var results = rulePair.Value.Rule.Validate(avatar);
                return new KeyValuePair<int, IEnumerable<ValidateResult>>(rulePair.Key, results);
            }).ToDictionary(resultPair => resultPair.Key, resultPair => resultPair.Value);
        }

        public void FocusTarget(ValidateResult result)
        {
            Selection.activeObject = result.Target;
            EditorGUIUtility.PingObject(result.Target);
        }

        public bool OnBuildRequested(VRCSDKRequestedBuildType requestedBuildType)
        {
            if (!ValidatorSettingsService.GetOrCreateSettings().validateOnUploadAvatar) return true;

            // アバターが取得できなかったときValidationしない
            var avatarGameObject = Selection.activeObject as GameObject;
            if (avatarGameObject == null) return true;

            avatar = avatarGameObject.GetComponent<VRCAvatarDescriptor>();
            if (avatar == null) return true;

            resultDictionary = ValidateAvatars3(avatar, ruleDictionary);

            if (resultDictionary
                    .Any(result => result.Value.Any(
                        r => r.ResultType == ValidateResult.ValidateResultType.Error ||
                            r.ResultType == ValidateResult.ValidateResultType.Warning)))
            {
                Open();
                return false;
            }

            return true;
        }


        private void FetchRuleValidateEnabled()
        {
            if (validatorSettings == null)
            {
                validatorSettings = ValidatorSettingsService.GetOrCreateSettings();
            }
            var validateRuleDictionary = validatorSettings.validateRuleDictionary;

            for (int i = 0; i < ruleDictionary.Count; i++ )
            {
                var ruleId = i + 1;
                var rule = ruleDictionary[ruleId];
                var ruleName = rule.ToString().Split('.').Last();
                if (!validateRuleDictionary.TryGetValue(ruleName, out var enabled))
                {
                    enabled = true;
                }

                ruleDictionary[ruleId].Enabled = enabled;
            }
        }
    }
}
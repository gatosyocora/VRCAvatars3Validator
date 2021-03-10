using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace VRCAvatars3Validator
{
    public sealed class VRCAvatars3Validator : EditorWindow
    {
        private const string RULES_FOLDER_PATH = "Assets/VRCAvatars3Validator/Editor/Rules";

        private VRCAvatarDescriptor avatar;

        private Dictionary<int, IEnumerable<ValidateResult>> resultDictionary;

        private Vector2 scrollPos = Vector2.zero;

        private readonly Dictionary<int, RuleItem> ruleDictionary = new Dictionary<int, RuleItem>();

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
            var rules = Directory.EnumerateFiles(RULES_FOLDER_PATH, "*.cs", SearchOption.AllDirectories)
                                    .Select((filePath, index) =>
                                    {
                                        var ruleAsset = AssetDatabase.LoadAssetAtPath<MonoScript>(filePath);
                                        var type = ruleAsset.GetClass();
                                        return Activator.CreateInstance(type) as IRule;
                                    })
                                    .ToArray();

            for (int i = 0; i < rules.Length; i++)
            {
                ruleDictionary.Add(i + 1, new RuleItem
                {
                    Enabled = true,
                    Rule = rules[i]
                });
            }
        }

        public void OnGUI()
        {
            EditorGUILayout.Space();

            avatar = EditorGUILayout.ObjectField("Avatar", avatar, typeof(VRCAvatarDescriptor), true) as VRCAvatarDescriptor;

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Rules", EditorStyles.boldLabel);

            using (new EditorGUI.IndentLevelScope())
            {
                //for (int i = 0; i < ruleDictionary.Count; i++)
                //{
                //    ruleDictionary[i].Enabled = EditorGUILayout.ToggleLeft($"[{ruleDictionary.}]{ruleDictionary[i].Rule.RuleSummary}", ruleDictionary[i].Enabled);
                //}
                foreach (var ruleItem in ruleDictionary)
                {
                    ruleItem.Value.Enabled = EditorGUILayout.ToggleLeft($"[{ruleItem.Key}]{ruleItem.Value.Rule.RuleSummary}", ruleItem.Value.Enabled);
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
                            using (new EditorGUILayout.HorizontalScope())
                            {
                                EditorGUILayout.HelpBox($"[{ruleId}]{result}",
                                    result.ResultType == ValidateResult.ValidateResultType.Error ? MessageType.Error :
                                    result.ResultType == ValidateResult.ValidateResultType.Warning ? MessageType.Warning : MessageType.None);

                                using (new EditorGUILayout.VerticalScope(GUILayout.Width(60f)))
                                {
                                    if (GUILayout.Button("Select", GUILayout.Width(60f)))
                                    {
                                        result.FocusTarget();
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
                    }
                }
            }
            else
            {
                EditorGUILayout.HelpBox("No Error", MessageType.Info);
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
    }
}
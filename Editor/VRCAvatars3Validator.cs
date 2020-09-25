using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRCAvatars3Validator.Rules;

namespace VRCAvatars3Validator
{
    public class VRCAvatars3Validator : EditorWindow 
    {
        private VRCAvatarDescriptor avatar;

        private bool[] enableRules;

        private RuleBase[] rules;
        private List<Error> errors;

        private Vector2 scrollPos = Vector2.zero;

        [MenuItem("VRCAvatars3Validator/Editor")]
        public static void Open()
        {
            GetWindow<VRCAvatars3Validator>(nameof(VRCAvatars3Validator));
        }

        public void OnEnable()
        {
            rules = new RuleBase[]
            {
                new TestRule("01"),
                new ControllerLayerWeightRule("02"),
                new HaveExParamsInControllersRule("03")
            };

            enableRules = Enumerable.Range(0, rules.Length).Select(_ => true).ToArray();
        }

        public void OnGUI()
        {
            EditorGUILayout.Space();

            avatar = EditorGUILayout.ObjectField("Avatar", avatar, typeof(VRCAvatarDescriptor), true) as VRCAvatarDescriptor;

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Rules", EditorStyles.boldLabel);

            using (new EditorGUI.IndentLevelScope())
            {
                for (int i = 0; i < rules.Length; i++)
                {
                    enableRules[i] = EditorGUILayout.ToggleLeft($"[{rules[i].Id}]{rules[i].RuleSummary}", enableRules[i]);
                }
            }

            EditorGUILayout.Space();

            using (new EditorGUI.DisabledGroupScope(avatar is null))
            {
                if (GUILayout.Button("Validate"))
                {
                    errors = ValidateAvatars3(avatar, rules.Where((r, i) => enableRules[i]));
                }
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Errors", EditorStyles.boldLabel);

            if (errors is null) return;

            if (errors.Any())
            {
                using (var scroll = new EditorGUILayout.ScrollViewScope(scrollPos))
                {
                    scrollPos = scroll.scrollPosition;
                    foreach (var error in errors)
                    {
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            EditorGUILayout.HelpBox($"[{error.RuleId}]{error.Result}", MessageType.Error);

                            using (new EditorGUILayout.VerticalScope(GUILayout.Width(60f)))
                            {
                                if (GUILayout.Button("Select", GUILayout.Width(60f)))
                                {
                                    error.FocusTarget();
                                }

                                using (new EditorGUI.DisabledGroupScope(!error.CanAutoFix))
                                {
                                    if (GUILayout.Button("AutoFix", GUILayout.Width(60f)))
                                    {
                                        error.AutoFix();
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

        private List<Error> ValidateAvatars3(VRCAvatarDescriptor avatar, IEnumerable<RuleBase> rules)
        {
            var errors = new List<Error>();
            foreach (var rule in rules)
            {
                errors.AddRange(rule.Validate(avatar));
            }

            return errors;
        }
    }
}
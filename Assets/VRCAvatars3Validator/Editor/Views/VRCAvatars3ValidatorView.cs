using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
#if VRC_SDK_VRCSDK3
using VRCAvatarDescriptor = VRC.SDK3.Avatars.Components.VRCAvatarDescriptor;
#else
using VRCAvatarDescriptor = VRCAvatars3Validator.Mocks.VRCAvatarDescriptorMock;
#endif
using VRCAvatars3Validator.Models;
using VRCAvatars3Validator.Utilities;
using VRCAvatars3Validator.ViewModels;

namespace VRCAvatars3Validator.Views
{
    public class VRCAvatars3ValidatorView : EditorWindow
    {
        private VRCAvatars3ValidatorViewModel _viewModel = new VRCAvatars3ValidatorViewModel();

        private Vector2 scrollPos = Vector2.zero;

#if VRC_SDK_VRCSDK3
        [MenuItem("VRCAvatars3Validator/Editor")]
#endif
        public static void Open()
        {
            GetWindow<VRCAvatars3ValidatorView>(VRCAvatars3ValidatorViewModel.EDITOR_NAME);
        }

        private void OnOpen() => _viewModel.OnOpen();

        public void OnGUI()
        {
            OnOpen();

            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Settings"))
                {
                    _viewModel.OnSettingsClick();
                }
            }

            using (var check = new EditorGUI.ChangeCheckScope())
            {
                var avatar = EditorGUILayout.ObjectField(
                                "Avatar",
                                _viewModel.avatar,
                                typeof(VRCAvatarDescriptor),
                                true) as VRCAvatarDescriptor;

                if (check.changed)
                {
                    _viewModel.OnSwitchAvatar(avatar);
                }
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Rules", EditorStyles.boldLabel);

            using (new EditorGUI.IndentLevelScope())
            {
                for (int i = 0; i < _viewModel.settings.rules.Count; i++)
                {
                    var rule = RuleUtility.FilePath2IRule(_viewModel.settings.rules[i].FilePath);
                    var enabled = EditorGUILayout.ToggleLeft(
                                                            $"[{i + 1}] {rule.RuleSummary}",
                                                            _viewModel.settings.rules[i].Enabled);
                    if (enabled != _viewModel.settings.rules[i].Enabled) {
                        EditorUtility.SetDirty(_viewModel.settings);
                        _viewModel.settings.rules[i].Enabled = enabled;
                    }
                }
            }

            EditorGUILayout.Space();

            using (new EditorGUI.DisabledGroupScope(_viewModel.IsSelectionAvatar()))
            {
                if (GUILayout.Button("Validate"))
                {
                    _viewModel.OnValidateClick();
                }
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Errors", EditorStyles.boldLabel);

            if (_viewModel.HasNeverValided) return;

            if (_viewModel.ExistValidationResult())
            {
                using (var scroll = new EditorGUILayout.ScrollViewScope(scrollPos))
                {
                    scrollPos = scroll.scrollPosition;
                    foreach (var resultPair in _viewModel.resultDictionary)
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
                        _viewModel.OnSelectClick(result);
                    }

                    using (new EditorGUI.DisabledGroupScope(!result.CanAutoFix))
                    {
                        if (GUILayout.Button("AutoFix", GUILayout.Width(60f)))
                        {
                            _viewModel.OnAutoFixClick(result);
                        }
                    }
                }
            }
        }
    }
}

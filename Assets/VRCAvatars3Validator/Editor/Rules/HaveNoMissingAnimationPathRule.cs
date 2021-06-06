using System.Collections.Generic;
using UnityEditor;
#if VRC_SDK_VRCSDK3
using VRCAvatarDescriptor = VRC.SDK3.Avatars.Components.VRCAvatarDescriptor;
#else
using VRCAvatarDescriptor = VRCAvatars3Validator.Mocks.VRCAvatarDescriptorMock;
#endif
using VRCAvatars3Validator.Models;
using VRCAvatars3Validator.Utilities;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace VRCAvatars3Validator.Rules
{
    /// <summary>
    /// Validate if have missing path in AnimationClips
    /// </summary>
    public class HaveNoMissingAnimationPathRule : IRule, Settingable
    {
        public string RuleSummary => Localize.Translate("HaveNoMissingAnimationPathRule_summary");

        public static List<string> ignoreAnimationFileRegexsDefault
        {
            get => new List<string>
            {
                "^proxy_*"
            };
        }
        
        public IEnumerable<ValidateResult> Validate(VRCAvatarDescriptor avatar, RuleItemOptions options)
        {
            var animationClips = VRCAvatarUtility.GetAnimationClips(avatar);
            var ignoreAnimationFileRegexs = (options.ReadOptions<HaveNoMissingAnimationPathRuleOptions>() ?? new HaveNoMissingAnimationPathRuleOptions()).IgnoreAnimationFileRegexs;
            foreach (var animationClip in animationClips)
            {
                var fileName = Path.GetFileName(AssetDatabase.GetAssetPath(animationClip));
                if (ignoreAnimationFileRegexs.Any(regex => Regex.IsMatch(fileName, regex))) {
                    continue;
                }
                foreach (var binding in AnimationUtility.GetCurveBindings(animationClip))
                {
                    if (!avatar.transform.Find(binding.path))
                    {
                        yield return new ValidateResult(
                            animationClip,
                            ValidateResult.ValidateResultType.Warning,
                            Localize.Translate("HaveNoMissingAnimationPathRule_result", animationClip.name, binding.path));
                    }
                }
            }
        }

        public void OnGUI(ValidatorSettings settings, RuleItemOptions options) {
            EditorGUILayout.LabelField(Localize.Translate("HaveNoMissingAnimationPathRule_options_ignoreFileNameRegex"));
            using (new EditorGUILayout.HorizontalScope()) {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button(Localize.Translate("Add"))) {
                    options.ChangeOptions<HaveNoMissingAnimationPathRuleOptions>(option => option.IgnoreAnimationFileRegexs.Add(""));
                }
                if (GUILayout.Button(Localize.Translate("Reset"))) {
                    options.ChangeOptions<HaveNoMissingAnimationPathRuleOptions>(option => option.IgnoreAnimationFileRegexs = ignoreAnimationFileRegexsDefault);
                }
            }
            var haveNoMissingAnimationPathRuleOption = options.ReadOptions<HaveNoMissingAnimationPathRuleOptions>();
            if (haveNoMissingAnimationPathRuleOption == null) return;
            var ignoreAnimationFileRegexs = haveNoMissingAnimationPathRuleOption.IgnoreAnimationFileRegexs;
            for (int i = 0; i < ignoreAnimationFileRegexs.Count; i++) {
                var ignoreAnimationFileRegex = ignoreAnimationFileRegexs[i];

                using (new EditorGUILayout.HorizontalScope())
                using (var check = new EditorGUI.ChangeCheckScope()) {
                    ignoreAnimationFileRegexs[i] = EditorGUILayout.TextField(ignoreAnimationFileRegex);

                    if (check.changed) {
                        options.ChangeOptions<HaveNoMissingAnimationPathRuleOptions>(option => option.IgnoreAnimationFileRegexs = ignoreAnimationFileRegexs);
                    }

                    if (GUILayout.Button("×")) {
                        ignoreAnimationFileRegexs.RemoveAt(i);
                        options.ChangeOptions<HaveNoMissingAnimationPathRuleOptions>(option => option.IgnoreAnimationFileRegexs = ignoreAnimationFileRegexs);
                    }
                }
            }
        }

        class HaveNoMissingAnimationPathRuleOptions {
            public List<string> IgnoreAnimationFileRegexs = ignoreAnimationFileRegexsDefault;
        }
    }
}
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
        public string RuleSummary => "Exists missing path in AnimationClips";

        public IEnumerable<ValidateResult> Validate(VRCAvatarDescriptor avatar, ValidatorSettings settings, RuleItemOptions options)
        {
            var animationClips = VRCAvatarUtility.GetAnimationClips(avatar);
            var ignoreAnimationFileRegexs = (ruleItem.ReadOptions<Options>() ?? new Options()).IgnoreAnimationFileRegexs;
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
                            $"`{animationClip.name}` have missing path. ({binding.path})");
                    }
                }
            }
        }

        public void OnGUI(ValidatorSettings settings, RuleItem ruleItem) {
            using (new EditorGUILayout.HorizontalScope()) {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Add")) {
                    ruleItem.ChangeOptions<Options>(options => options.IgnoreAnimationFileRegexs.Add(""));
                    Debug.Log(ruleItem.Options);
                    EditorUtility.SetDirty(settings);
                }
                if (GUILayout.Button("Reset")) {
                    ruleItem.ChangeOptions<Options>(options => options.IgnoreAnimationFileRegexs = ignoreAnimationFileRegexsDefault);
                    EditorUtility.SetDirty(settings);
                }
            }
            var ignoreAnimationFileRegexs = ruleItem.ReadOptions<Options>().IgnoreAnimationFileRegexs;
            for (int i = 0; i < ignoreAnimationFileRegexs.Count; i++) {
                var ignoreAnimationFileRegex = ignoreAnimationFileRegexs[i];

                using (new EditorGUILayout.HorizontalScope())
                using (var check = new EditorGUI.ChangeCheckScope()) {
                    ignoreAnimationFileRegexs[i] = EditorGUILayout.TextField(ignoreAnimationFileRegex);

                    if (check.changed) {
                        ruleItem.ChangeOptions<Options>(options => options.IgnoreAnimationFileRegexs = ignoreAnimationFileRegexs);
                        EditorUtility.SetDirty(settings);
                    }

                    if (GUILayout.Button("×")) {
                        ignoreAnimationFileRegexs.RemoveAt(i);
                        ruleItem.ChangeOptions<Options>(options => options.IgnoreAnimationFileRegexs = ignoreAnimationFileRegexs);
                        EditorUtility.SetDirty(settings);
                    }
                }
            }
        }

        class Options {
            public List<string> IgnoreAnimationFileRegexs;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
#if VRC_SDK_VRCSDK3
using VRCAvatarDescriptor = VRC.SDK3.Avatars.Components.VRCAvatarDescriptor;
#else
using VRCAvatarDescriptor = VRCAvatars3Validator.Mocks.VRCAvatarDescriptorMock;
#endif
using UnityEditor.Animations;
using UnityEditor;
using VRCAvatars3Validator.Utilities;
using VRCAvatars3Validator.Models;

namespace VRCAvatars3Validator.Rules
{
    public class HaveTransformAnimationRule : IRule
    {
        public string RuleSummary => Localize.Translate("HaveTransformAnimationRule_summary");

        public RuleItemOptions Options => null;

        private static string[] humanoidBoneNamesMissingInHumanBodyBones = new string[]
        {
            "Left Arm ",
            "Left Forearm ",
            "Right Arm ",
            "Right Forearm ",
            "RootT",
            "RootQ"
        };

        public IEnumerable<ValidateResult> Validate(VRCAvatarDescriptor avatar, RuleItemOptions options)
        {
            var humanoidBoneNames = Enum.GetNames(typeof(HumanBodyBones))
                                        .SelectMany(n => new string[] { n, ToContainSpace(n) })
                                        .Concat(humanoidBoneNamesMissingInHumanBodyBones)
                                        .Distinct()
                                        .ToArray();

            var playableLayers = VRCAvatarUtility.GetBaseAnimationLayers(avatar);

            if (!playableLayers.Any()) yield break;

            foreach (var playableLayer in playableLayers)
            {
                if (playableLayer.type == VRCAvatarDescriptor.AnimLayerType.FX) continue;

                var animatorController = playableLayer.animatorController as AnimatorController;

                if (animatorController is null) continue;

                foreach (var state in animatorController.layers.SelectMany(l => l.stateMachine.states))
                {
                    var clip = state.state.motion as AnimationClip;

                    if (clip is null) continue;

                    foreach (var binding in AnimationUtility.GetCurveBindings(clip))
                    {
                        // Transformを操作するもの以外が含まれているか検出
                        if (binding.type != typeof(Transform) &&
                            !(binding.type == typeof(Animator) && humanoidBoneNames.Any(n => binding.propertyName.StartsWith(n))))
                        {
                            yield return new ValidateResult(
                                clip,
                                ValidateResult.ValidateResultType.Error,
                                Localize.Translate("HaveTransformAnimationRule_result", clip.name));
                        }
                    }
                }
            }
        }

        private string ToContainSpace(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            int startIndex = 0, count = 1;
            var words = new List<string>();
            // 最初が小文字の可能性があるため1から
            for (int i = 1; i < input.Length; i++)
            {
                if (char.IsUpper(input[i]))
                {
                    words.Add(input.Substring(startIndex, count));
                    startIndex = i;
                    count = 1;
                }
                else count++;
            }
            words.Add(input.Substring(startIndex, count));

            return string.Join(" ", words);
        }
    }
}
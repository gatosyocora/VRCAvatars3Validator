using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using UnityEditor.Animations;
using UnityEditor;
using VRCAvatars3Validator.Utilities;
using VRCAvatars3Validator.Models;

namespace VRCAvatars3Validator.Rules
{
    public class HaveTransformAnimationRule : IRule
    {
        public string RuleSummary => "Have other than Transform Animation in other than FX";

        public IEnumerable<ValidateResult> Validate(VRCAvatarDescriptor avatar)
        {
            var humanoidBoneNames = Enum.GetNames(typeof(HumanBodyBones))
                                        .SelectMany(n => new string[] { n, ToContainSpace(n) })
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
                                $"{clip.name} have key changed other than Transform");
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
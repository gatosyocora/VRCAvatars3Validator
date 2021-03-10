using System.Collections.Generic;
using UnityEditor;
using VRC.SDK3.Avatars.Components;
using VRCAvatars3Validator.Utilities;

namespace VRCAvatars3Validator.Rules
{
    /// <summary>
    /// Validate if have missing path in AnimationClips
    /// </summary>
    public class HaveNoMissingAnimationPathRule : IRule
    {
        public string RuleSummary => "Exists missing path in AnimationClips";

        public IEnumerable<ValidateResult> Validate(VRCAvatarDescriptor avatar)
        {
            var animationClips = VRCAvatarUtility.GetAnimationClips(avatar);
            foreach (var animationClip in animationClips)
            {
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
    }
}
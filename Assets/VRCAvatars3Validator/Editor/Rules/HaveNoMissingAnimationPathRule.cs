using System.Collections.Generic;
using UnityEditor;
#if VRC_SDK_VRCSDK3
using VRCAvatarDescriptor = VRC.SDK3.Avatars.Components.VRCAvatarDescriptor;
#else
using VRCAvatarDescriptor = VRCAvatars3Validator.Mocks.VRCAvatarDescriptorMock;
#endif
using VRCAvatars3Validator.Models;
using VRCAvatars3Validator.Utilities;

namespace VRCAvatars3Validator.Rules
{
    /// <summary>
    /// Validate if have missing path in AnimationClips
    /// </summary>
    public class HaveNoMissingAnimationPathRule : IRule
    {
        public string RuleSummary => Localize.Translate("HaveNoMissingAnimationPathRule_summary");

        public IEnumerable<ValidateResult> Validate(VRCAvatarDescriptor avatar, ValidatorSettings settings)
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
                            Localize.Translate("HaveNoMissingAnimationPathRule_result", animationClip.name, binding.path));
                    }
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
#if VRC_SDK_VRCSDK3
using VRCAvatarDescriptor = VRC.SDK3.Avatars.Components.VRCAvatarDescriptor;
using VRCExpressionParameters = VRC.SDK3.Avatars.ScriptableObjects.VRCExpressionParameters;
#else
using VRCAvatarDescriptor = VRCAvatars3Validator.Mocks.VRCAvatarDescriptorMock;
using VRCExpressionParameters = VRCAvatars3Validator.Mocks.VRCExpressionParametersMock;
#endif

namespace VRCAvatars3Validator.Utilities
{
    public static class VRCAvatarUtility
    {
        public static IEnumerable<AnimatorController> GetControllers(VRCAvatarDescriptor avatar)
                        => avatar.baseAnimationLayers
                            .Union(avatar.specialAnimationLayers)
                            .Select(l => l.animatorController as AnimatorController)
                            .Where(c => c != null);

        public static IEnumerable<VRCAvatarDescriptor.CustomAnimLayer> GetBaseAnimationLayers(VRCAvatarDescriptor avatar)
                        => avatar.baseAnimationLayers
                            .Where(l => l.animatorController != null);

        public static IEnumerable<AnimatorControllerParameter> GetParameters(IEnumerable<AnimatorController> controllers)
                        => controllers
                            .Where(c => c != null)
                            .SelectMany(c => c.parameters);

        public static VRCExpressionParameters GetExpressionParametersAsset(VRCAvatarDescriptor avatar)
                        => avatar.expressionParameters;

        public static IEnumerable<AnimationClip> GetAnimationClips(VRCAvatarDescriptor avatar)
                        => GetControllers(avatar)
                            .SelectMany(controller => controller.animationClips);
    }
}
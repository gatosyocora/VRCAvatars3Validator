using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;

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
    }
}
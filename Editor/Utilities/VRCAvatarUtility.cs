using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace VRCAvatars3Validator.Utilities
{
    public static class VRCAvatarUtility
    {
        public static AnimatorController[] GetControllers(VRCAvatarDescriptor avatar)
            => avatar.baseAnimationLayers
                .Union(avatar.specialAnimationLayers)
                .Select(l => l.animatorController as AnimatorController)
                .Where(c => c != null)
                .ToArray();
    }
}
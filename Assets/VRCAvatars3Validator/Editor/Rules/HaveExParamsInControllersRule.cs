using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
using VRCAvatars3Validator.Models;
using VRCAvatars3Validator.Utilities;

namespace VRCAvatars3Validator.Rules
{
    /// <summary>
    /// ExpressionParametersに設定されているAnimatorParameterがどのControllerにもない
    /// </summary>
    public class HaveExParamsInControllersRule : IRule
    {
        public string RuleSummary => "Missing Expression Parameter";

        public IEnumerable<ValidateResult> Validate(VRCAvatarDescriptor avatar)
        {
            var exParamsAsset = VRCAvatarUtility.GetExpressionParametersAsset(avatar);

            if (exParamsAsset is null) yield break;

            var exParams = exParamsAsset.parameters.Where(p => !string.IsNullOrEmpty(p.name)).ToArray();

            if (!exParams.Any()) yield break;

            var parameterlist = VRCAvatarUtility.GetParameters(avatar.baseAnimationLayers.Select(l => l.animatorController as AnimatorController));

            bool found = false;
            foreach (var exParam in exParams)
            {
                var exParamName = exParam.name;
                var exParamType = exParam.valueType == VRCExpressionParameters.ValueType.Float ?
                                    AnimatorControllerParameterType.Float :
                                    AnimatorControllerParameterType.Int;

                found = false;
                foreach (var param in parameterlist)
                {
                    if (exParamName == param.name && exParamType == param.type)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    yield return new ValidateResult(
                                    exParamsAsset,
                                    ValidateResult.ValidateResultType.Error,
                                    $"{exParamName} is not found in AnimatorControllers");
                }
            }
        }
    }
}
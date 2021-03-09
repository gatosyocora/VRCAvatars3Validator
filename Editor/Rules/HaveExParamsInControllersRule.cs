using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;

namespace VRCAvatars3Validator.Rules
{
    /// <summary>
    /// ExpressionParametersに設定されているAnimatorParameterがどのControllerにもない
    /// </summary>
    public class HaveExParamsInControllersRule : RuleBase
    {
        public override string RuleSummary => "Missing Expression Parameter";

        public HaveExParamsInControllersRule(string id) : base(id) { }

        public override IEnumerable<ValidateResult> Validate(VRCAvatarDescriptor avatar)
        {
            var exParamsAsset = avatar.expressionParameters;

            if (exParamsAsset is null) yield break;

            var exParams = exParamsAsset.parameters.Where(p => !string.IsNullOrEmpty(p.name)).ToArray();

            if (!exParams.Any()) yield break;

            var parameterlist = GetParameters(avatar.baseAnimationLayers.Select(l => l.animatorController).Where(l => l != null).ToArray());

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
                                Id,
                                exParamsAsset,
                                ValidateResult.ValidateResultType.Error,
                                $"{exParamName} is not found in AnimatorControllers");
                }
            }
        }

        private IEnumerable<AnimatorControllerParameter> GetParameters(IEnumerable<RuntimeAnimatorController> controllers)
        {
            foreach (AnimatorController controller in controllers)
            {
                if (controller is null) continue;

                foreach (var parameter in controller.parameters)
                {
                    yield return parameter;
                }
            }
        }
    }
}
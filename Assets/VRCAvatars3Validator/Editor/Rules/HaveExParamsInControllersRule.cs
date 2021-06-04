using System;
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
using VRCAvatars3Validator.Models;
using VRCAvatars3Validator.Utilities;

namespace VRCAvatars3Validator.Rules
{
    /// <summary>
    /// ExpressionParametersに設定されているAnimatorParameterがどのControllerにもない
    /// </summary>
    public class HaveExParamsInControllersRule : IRule
    {
        public string RuleSummary => Localize.Translate("HaveExParamsInControllersRule_summary");

        public IEnumerable<ValidateResult> Validate(VRCAvatarDescriptor avatar, ValidatorSettings settings)
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
                var exParamType = ParamTypeMap[exParam.valueType];

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
                                    Localize.Translate("HaveExParamsInControllersRule_result", exParamName));
                }
            }
        }

        Dictionary<VRCExpressionParameters.ValueType, AnimatorControllerParameterType> ParamTypeMap =
            new Dictionary<VRCExpressionParameters.ValueType, AnimatorControllerParameterType> {
                { VRCExpressionParameters.ValueType.Int, AnimatorControllerParameterType.Int },
                { VRCExpressionParameters.ValueType.Float, AnimatorControllerParameterType.Float },
                { VRCExpressionParameters.ValueType.Bool, AnimatorControllerParameterType.Bool },
            };
    }
}

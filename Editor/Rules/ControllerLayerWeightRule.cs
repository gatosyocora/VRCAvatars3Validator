using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace VRCAvatars3Validator.Rules
{
    /// <summary>
    /// Weight0のレイヤーを持つかどうか
    /// </summary>
    public class ControllerLayerWeightRule : RuleBase
    {
        public override string RuleSummary => "Have weight 0 layer";

        public ControllerLayerWeightRule(string id) : base(id) { }

        public override IEnumerable<ValidateResult> Validate(VRCAvatarDescriptor avatar)
        {
            var controllers = avatar.baseAnimationLayers
                                .Select(l => l.animatorController);

            if (!controllers.Any()) yield break;

            foreach (AnimatorController controller in controllers)
            {
                if (controller is null) continue;

                // 一番上のLayerは内部的にweight0であっても強制的に1になるので調べない
                for (int i = 1; i < controller.layers.Length; i++)
                {
                    var layer = controller.layers[i];
                    if (layer.defaultWeight == 0)
                    {
                        yield return new ValidateResult(
                                    Id,
                                    controller,
                                    ValidateResult.ValidateResultType.Warning,
                                    $"{layer.name} Layer in {controller.name} is weight 0.");
                    }
                }
            }
        }
    }
}
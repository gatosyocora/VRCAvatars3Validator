using System.Collections.Generic;
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
    /// Weight0のレイヤーを持つかどうか
    /// </summary>
    public class ControllerLayerWeightRule : IRule
    {
        public string RuleSummary => "Have weight 0 layer";

        public IEnumerable<ValidateResult> Validate(VRCAvatarDescriptor avatar, ValidatorSettings settings, RuleItem ruleItem)
        {
            var controllers = VRCAvatarUtility.GetControllers(avatar);

            foreach (var controller in controllers)
            {
                // 一番上のLayerは内部的にweight0であっても強制的に1になるので調べない
                for (int i = 1; i < controller.layers.Length; i++)
                {
                    var layer = controller.layers[i];
                    if (layer.defaultWeight == 0)
                    {
                        yield return new ValidateResult(
                                        controller,
                                        ValidateResult.ValidateResultType.Warning,
                                        $"`{layer.name}` Layer in {controller.name} is weight 0.");
                    }
                }
            }
        }
    }
}
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
        public string RuleSummary => Localize.Translate("ControllerLayerWeightRule_summary");

        public RuleItemOptions Options => null;

        public IEnumerable<ValidateResult> Validate(VRCAvatarDescriptor avatar, RuleItemOptions options)
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
                                        Localize.Translate("ControllerLayerWeightRule_result", layer.name, controller.name));
                    }
                }
            }
        }
    }
}
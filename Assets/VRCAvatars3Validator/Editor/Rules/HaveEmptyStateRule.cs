using System.Collections.Generic;
using VRCAvatars3Validator.Models;
using VRCAvatars3Validator.Utilities;
#if VRC_SDK_VRCSDK3
using VRCAvatarDescriptor = VRC.SDK3.Avatars.Components.VRCAvatarDescriptor;
#else
using VRCAvatarDescriptor = VRCAvatars3Validator.Mocks.VRCAvatarDescriptorMock;
#endif

namespace VRCAvatars3Validator.Rules
{
    public class HaveEmptyStateRule : IRule
    {
        public string RuleSummary => "Have empty state";

        public IEnumerable<ValidateResult> Validate(VRCAvatarDescriptor avatar, RuleItemOptions options)
        {
            var result = new List<ValidateResult>();

            var controllers = VRCAvatarUtility.GetControllers(avatar);

            foreach (var controller in controllers)
            {
                foreach (var layer in controller.layers)
                {
                    foreach (var childState in layer.stateMachine.states)
                    {
                        if (childState.state.motion == null)
                        {
                            result.Add(new ValidateResult(
                                controller,
                                ValidateResult.ValidateResultType.Error,
                                $"{childState.state.name} has not animation clip in {layer.name} layer of {controller}"
                            ));
                        }
                    }
                }
            }

            return result;
        }
    }
}
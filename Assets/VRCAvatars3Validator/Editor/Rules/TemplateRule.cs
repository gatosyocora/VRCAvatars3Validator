using System.Collections.Generic;
#if VRC_SDK_VRCSDK3
using VRCAvatarDescriptor = VRC.SDK3.Avatars.Components.VRCAvatarDescriptor;
#else
using VRCAvatarDescriptor = VRCAvatars3Validator.Mocks.VRCAvatarDescriptorMock;
#endif
using VRCAvatars3Validator.Models;

namespace VRCAvatars3Validator.Rules
{
    /// <summary>
    /// Write rule summary.
    /// </summary>
    public class TemplateRule : IRule
    {
        public string RuleSummary => "Write rule summary.";

        public IEnumerable<ValidateResult> Validate(VRCAvatarDescriptor avatar)
        {
            // Write validation code.
            // Return validation result if have error.
            yield return new ValidateResult(
                null,
                ValidateResult.ValidateResultType.Error,
                "Write result message.");
        }
    }
}
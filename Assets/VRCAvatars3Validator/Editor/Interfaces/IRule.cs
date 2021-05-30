using System.Collections.Generic;
#if VRC_SDK_VRCSDK3
using VRCAvatarDescriptor = VRC.SDK3.Avatars.Components.VRCAvatarDescriptor;
#else
using VRCAvatarDescriptor = VRCAvatars3Validator.Mocks.VRCAvatarDescriptorMock;
#endif
using VRCAvatars3Validator.Models;

namespace VRCAvatars3Validator
{
    public interface IRule
    {
        /// <summary>
        /// Rule summary.
        /// </summary>
        string RuleSummary { get; }

        /// <summary>
        /// Run validation by defined rules.
        /// </summary>
        /// <param name="avatar">Validated avatar</param>
        /// <returns>Validate results</returns>
        IEnumerable<ValidateResult> Validate(VRCAvatarDescriptor avatar, ValidatorSettings settings);
    }
}
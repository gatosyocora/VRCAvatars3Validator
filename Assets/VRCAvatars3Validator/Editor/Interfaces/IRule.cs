using System.Collections.Generic;
using VRC.SDK3.Avatars.Components;
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
        IEnumerable<ValidateResult> Validate(VRCAvatarDescriptor avatar);
    }
}
using System.Collections.Generic;
using System.Linq;
#if VRC_SDK_VRCSDK3
using VRCAvatarDescriptor = VRC.SDK3.Avatars.Components.VRCAvatarDescriptor;
#else
using VRCAvatarDescriptor = VRCAvatars3Validator.Mocks.VRCAvatarDescriptorMock;
#endif
using VRCAvatars3Validator.Models;
using VRCAvatars3Validator.Utilities;

namespace VRCAvatars3Validator
{
    public sealed class VRCAvatars3Validator
    {
        public static Dictionary<int, IEnumerable<ValidateResult>> ValidateAvatars3(VRCAvatarDescriptor avatar, IEnumerable<RuleItem> rules)
        {
            if (avatar == null)
                return new Dictionary<int, IEnumerable<ValidateResult>>();
            return rules.Select((rule, index) => new { Rule = rule, Index = index})
                .Where(rulePair => rulePair.Rule.Enabled)
                .Select(rulePair =>
                {
                    var rule = RuleUtility.FilePath2IRule(rulePair.Rule.FilePath);
                    var results = rule.Validate(avatar, rulePair.Rule.Options);
                    return new KeyValuePair<int, IEnumerable<ValidateResult>>(rulePair.Index + 1, results);
                })
                .ToDictionary(resultPair => resultPair.Key, resultPair => resultPair.Value);
        }
    }
}
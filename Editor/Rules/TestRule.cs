using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace VRCAvatars3Validator.Rules
{
    /// <summary>
    /// Test用のルール
    /// </summary>
    public class TestRule : RuleBase
    {
        public override string RuleSummary => "Test Rule";

        public TestRule(string id) : base(id) { }

        public override IEnumerable<ValidateResult> Validate(VRCAvatarDescriptor avatar)
        {
            return Enumerable.Range(0, 3)
                        .Select(i =>
                        new ValidateResult(
                            Id,
                            avatar.gameObject,
                            $"Test Error {i}",
                            "Press AutoFix",
                            () => Debug.Log($"Test{i}")))
                        .ToArray();
        }
    }
}
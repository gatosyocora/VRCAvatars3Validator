using NUnit.Framework;
using System.Linq;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRCAvatars3Validator.Rules;

namespace VRCAvatars3Validator.Tests
{
    public class TestRuleTest
    {
        [Test]
        public void TargetMatchAvatarObject()
        {
            var avatarObject = new GameObject();
            var avatar = avatarObject.AddComponent<VRCAvatarDescriptor>();

            var results = new TestRule().Validate(avatar).ToArray();
            for (int i = 0; i < results.Length; i++)
            {
                Assert.AreEqual(avatarObject, results[i].Target);
            }
        }

        [Test]
        public void ResuleTypeIsWarnning()
        {
            var avatarObject = new GameObject();
            var avatar = avatarObject.AddComponent<VRCAvatarDescriptor>();

            var results = new TestRule().Validate(avatar).ToArray();
            for (int i = 0; i < results.Length; i++)
            {
                Assert.AreEqual(ValidateResult.ValidateResultType.Warning, results[i].ResultType);
            }
        }

        [Test]
        public void ResultMessageIsExpected()
        {
            var avatarObject = new GameObject();
            var avatar = avatarObject.AddComponent<VRCAvatarDescriptor>();

            var results = new TestRule().Validate(avatar).ToArray();
            for (int i = 0; i < results.Length; i++)
            {
                Assert.AreEqual($"Test Error {i}", results[i].Result);
            }
        }

        [Test]
        public void SolutionMessageIsExpected()
        {
            var avatarObject = new GameObject();
            var avatar = avatarObject.AddComponent<VRCAvatarDescriptor>();

            var results = new TestRule().Validate(avatar).ToArray();
            for (int i = 0; i < results.Length; i++)
            {
                Assert.AreEqual("Press AutoFix", results[i].Solution);
            }
        }
    }
}


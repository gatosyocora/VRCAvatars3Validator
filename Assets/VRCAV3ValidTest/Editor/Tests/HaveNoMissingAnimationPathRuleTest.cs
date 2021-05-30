using NUnit.Framework;
using System.Linq;
using UnityEditor;
using UnityEngine;
#if VRC_SDK_VRCSDK3
using VRCAvatarDescriptor = VRC.SDK3.Avatars.Components.VRCAvatarDescriptor;
#else
using VRCAvatarDescriptor = VRCAvatars3Validator.Mocks.VRCAvatarDescriptorMock;
#endif
using VRCAvatars3Validator.Models;
using VRCAvatars3Validator.Rules;

namespace VRCAvatars3Validator.Tests
{
    public class HaveNoMissingAnimationPathRuleTest
    {
        [Test]
        public void MissingPathDetect()
        {
            var testAvatarGUID = "bf0785317e095d54483b43e4821470dd";
            var assetPath = AssetDatabase.GUIDToAssetPath(testAvatarGUID);

            var prefab = PrefabUtility.LoadPrefabContents(assetPath);
            var avatar = GameObject.Instantiate<GameObject>(prefab);
            var vrcAvatarDescriptor = avatar.GetComponent<VRCAvatarDescriptor>();

            var expecteds = new ValidateResult[]
            {
                new ValidateResult(AssetDatabase.LoadAssetAtPath<AnimationClip>(AssetDatabase.GUIDToAssetPath("2efc90e0d0024f24f9e72f6b6325138b")), ValidateResult.ValidateResultType.Warning, "`HaveMissing` have missing path. (Parent/Cube)"),
                new ValidateResult(AssetDatabase.LoadAssetAtPath<AnimationClip>(AssetDatabase.GUIDToAssetPath("7a10dddcd1fb33c48b79d0eafe93949f")), ValidateResult.ValidateResultType.Warning, "`HaveMissing2` have missing path. (Spher)"),
                new ValidateResult(AssetDatabase.LoadAssetAtPath<AnimationClip>(AssetDatabase.GUIDToAssetPath("7a10dddcd1fb33c48b79d0eafe93949f")), ValidateResult.ValidateResultType.Warning, "`HaveMissing2` have missing path. (Parent/Capsules)"),
            };

            var settings = ScriptableObject.CreateInstance<ValidatorSettings>();
            settings.rules.Add(new RuleItem());
            var results = new HaveNoMissingAnimationPathRule().Validate(vrcAvatarDescriptor, settings, settings.rules[0]).ToArray();

            Assert.AreEqual(3, results.Length);

            for (int i = 0; i < results.Length; i++)
            {
                var result = results[i];
                var expected = expecteds[i];

                Assert.AreEqual(expected.ResultMessage, result.ResultMessage);
                Assert.AreEqual(expected.ResultType, result.ResultType);
                Assert.AreEqual(expected.Target, result.Target);
                Assert.AreEqual(expected.SolutionMessage, result.SolutionMessage);
                Assert.AreEqual(expected.CanAutoFix, result.CanAutoFix);
                Assert.AreEqual(expected.AutoFix, result.AutoFix);
            }
        }
    }
}

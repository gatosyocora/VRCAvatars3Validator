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
    public class ExpressionsSubMenuRuleTest
    {
        [Test]
        public void UnsetSubMenuDetect()
        {
            var testAvatarGUID = "4af480d8b06d1e948a211660b844a55e";
            var assetPath = AssetDatabase.GUIDToAssetPath(testAvatarGUID);

            var prefab = PrefabUtility.LoadPrefabContents(assetPath);
            var avatar = GameObject.Instantiate<GameObject>(prefab);
            var vrcAvatarDescriptor = avatar.GetComponent<VRCAvatarDescriptor>();

            var expecteds = new ValidateResult[]
            {
                new ValidateResult(vrcAvatarDescriptor.expressionsMenu, ValidateResult.ValidateResultType.Error, "`Unset Sub Menu 1` exists unset SubMenu."),
                new ValidateResult(vrcAvatarDescriptor.expressionsMenu, ValidateResult.ValidateResultType.Error, "`Unset Sub Menu 2` exists unset SubMenu."),
                new ValidateResult(vrcAvatarDescriptor.expressionsMenu.controls[2].subMenu, ValidateResult.ValidateResultType.Error, "`Unset Sub Menu 3` exists unset SubMenu."),
                new ValidateResult(vrcAvatarDescriptor.expressionsMenu.controls[2].subMenu.controls[1].subMenu, ValidateResult.ValidateResultType.Error, "`Unset Sub Menu 4` exists unset SubMenu."),
            };

            var results = new ExpressionsSubMenuRule().Validate(vrcAvatarDescriptor).ToArray();

            Assert.AreEqual(4, results.Length);

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

using System.Linq;
using System.Collections.Generic;
#if VRC_SDK_VRCSDK3
using VRCAvatarDescriptor = VRC.SDK3.Avatars.Components.VRCAvatarDescriptor;
using ControlType = VRC.SDK3.Avatars.ScriptableObjects.VRCExpressionsMenu.Control.ControlType;
using VRCExpressionsMenu = VRC.SDK3.Avatars.ScriptableObjects.VRCExpressionsMenu;
#else
using VRCAvatarDescriptor = VRCAvatars3Validator.Mocks.VRCAvatarDescriptorMock;
using ControlType = VRCAvatars3Validator.Mocks.VRCExpressionsMenuMock.Control.ControlType;
using VRCExpressionsMenu = VRCAvatars3Validator.Mocks.VRCExpressionsMenuMock;
#endif

using ValidateResultType = VRCAvatars3Validator.Models.ValidateResult.ValidateResultType;
using VRCAvatars3Validator.Models;

namespace VRCAvatars3Validator.Rules
{
    /// <summary>
    /// Validate if exists unset SubMenu.
    /// </summary>
    public class ExpressionsSubMenuRule : IRule
    {
        public string RuleSummary => "Exists unset SubMenu";

        public IEnumerable<ValidateResult> Validate(VRCAvatarDescriptor avatar, ValidatorSettings settings, RuleItemOptions options)
        {
            if (avatar.expressionsMenu is null) return Enumerable.Empty<ValidateResult>();

            return ValidateExpressionsMenu(avatar.expressionsMenu);
        }

        private IEnumerable<ValidateResult> ValidateExpressionsMenu(VRCExpressionsMenu expressionsMenu)
        {
            foreach (var control in expressionsMenu.controls)
            {
                if (control.type != ControlType.SubMenu) continue;

                if (control.subMenu is null)
                {
                    yield return new ValidateResult(
                        expressionsMenu,
                        ValidateResultType.Error,
                        $"`{control.name}` exists unset SubMenu.");
                }
                else
                {
                    // Validating recursively.
                    foreach (var validateResult in ValidateExpressionsMenu(control.subMenu))
                    {
                        yield return validateResult;
                    }
                }
            }
        }
    }
}

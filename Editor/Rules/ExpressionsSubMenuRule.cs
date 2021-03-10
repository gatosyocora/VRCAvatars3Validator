using System.Linq;
using System.Collections.Generic;
using VRC.SDK3.Avatars.Components;

using ControlType = VRC.SDK3.Avatars.ScriptableObjects.VRCExpressionsMenu.Control.ControlType;
using ValidateResultType = VRCAvatars3Validator.ValidateResult.ValidateResultType;
using VRCExpressionsMenu = VRC.SDK3.Avatars.ScriptableObjects.VRCExpressionsMenu;

namespace VRCAvatars3Validator.Rules
{
    /// <summary>
    /// Validate if exists unset SubMenu.
    /// </summary>
    public class ExpressionsSubMenuRule : IRule
    {
        public string RuleSummary => "Exists unset SubMenu";

        public IEnumerable<ValidateResult> Validate(VRCAvatarDescriptor avatar)
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

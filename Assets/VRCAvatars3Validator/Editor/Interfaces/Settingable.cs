using VRCAvatars3Validator.Models;

namespace VRCAvatars3Validator
{
    public interface Settingable
    {
        object Options { get; }
        void OnGUI(RuleItemOptions options);
    }
}

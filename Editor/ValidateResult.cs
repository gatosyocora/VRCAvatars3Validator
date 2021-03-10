using System;
using Object = UnityEngine.Object;

namespace VRCAvatars3Validator
{
    public class ValidateResult
    {
        public enum ValidateResultType
        {
            Success, // No Problem
            Warning, // May need to fix
            Error, // Should fix
        }

        /// <summary>
        /// Fix target
        /// </summary>
        public Object Target { get; private set; }

        /// <summary>
        /// Fix proposal level
        /// </summary>
        public ValidateResultType ResultType { get; private set; }

        /// <summary>
        /// Result message
        /// </summary>
        public string Result { get; private set; }

        /// <summary>
        /// Way to fix
        /// </summary>
        public string Solution { get; private set; }

        /// <summary>
        /// Can use auto fix function
        /// </summary>
        public bool CanAutoFix { get => AutoFix != null; }

        /// <summary>
        /// auto fix function
        /// </summary>
        public Action AutoFix { get; private set; }

        public ValidateResult(Object target, ValidateResultType resultType, string result, string solution = "", Action autoFix = null)
        {
            Target = target;
            ResultType = resultType;
            Result = result;
            Solution = solution;
            AutoFix = autoFix;
        }
    }
}
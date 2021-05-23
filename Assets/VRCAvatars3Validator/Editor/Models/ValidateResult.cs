using System;
using Object = UnityEngine.Object;

namespace VRCAvatars3Validator.Models
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
        public string ResultMessage { get; private set; }

        /// <summary>
        /// Way to fix
        /// </summary>
        public string SolutionMessage { get; private set; }

        /// <summary>
        /// Can use auto fix function
        /// </summary>
        public bool CanAutoFix { get => AutoFix != null; }

        /// <summary>
        /// auto fix function
        /// </summary>
        public Action AutoFix { get; private set; }

        public ValidateResult(Object target, ValidateResultType resultType, string resultMessage, string solutionMessage = "", Action autoFix = null)
        {
            Target = target;
            ResultType = resultType;
            ResultMessage = resultMessage;
            SolutionMessage = solutionMessage;
            AutoFix = autoFix;
        }
    }
}
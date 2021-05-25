using System.Collections.Generic;
using UnityEngine;
using VRCExpressionsMenu = VRCAvatars3Validator.Mocks.VRCExpressionsMenuMock;

namespace VRCAvatars3Validator.Mocks
{
    public class VRCExpressionsMenuMock : ScriptableObject
    {
        public const int MAX_CONTROLS = 8;
        public List<Control> controls;

        //public VRCExpressionsMenu();

        public class Control
        {
            public string name;
            public Texture2D icon;
            public ControlType type;
            public Parameter parameter;
            public float value;
            public Style style;
            public VRCExpressionsMenu subMenu;
            public Parameter[] subParameters;
            public Label[] labels;

            //public Control();

            //public Label GetLabel(int i);
            //public Parameter GetSubParameter(int i);

            public enum ControlType
            {
                Button = 101,
                Toggle = 102,
                SubMenu = 103,
                TwoAxisPuppet = 201,
                FourAxisPuppet = 202,
                RadialPuppet = 203
            }
            public enum Style
            {
                Style1 = 0,
                Style2 = 1,
                Style3 = 2,
                Style4 = 3
            }

            public struct Label
            {
                public string name;
                public Texture2D icon;
            }

            public class Parameter
            {
                public string name;
                //public Parameter();
                public int hash { get; }
                //public static bool IsNull(Parameter parameter);
            }
        }
    }
}
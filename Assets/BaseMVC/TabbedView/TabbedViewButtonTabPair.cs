using System;

namespace MVC.RuntimeTabbedView
{
    public class TabbedViewButtonTabPair<ButtonType, TabType, EnumType>
        where EnumType : struct, IConvertible
        where ButtonType : TabbedViewButton<EnumType>
        where TabType : TabbedViewTab<ButtonType, EnumType>
    {
        public ButtonType Button { get; private set; }
        public TabType Tab { get; private set; }
        public EnumType BoundEnum { get; private set; }

        public TabbedViewButtonTabPair (ButtonType button, TabType tab, EnumType boundEnum)
        {
            Button = button;
            Tab = tab;
            BoundEnum = boundEnum;

            Button.BoundType = BoundEnum;
            Tab.BoundType = BoundEnum;
        }
    }
}


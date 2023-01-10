using System;

namespace MVC.RuntimeTabbedView
{
    public class TabbedViewController<EnumType, CreatedTabType, CreatedButtonType, TabbedViewViewType, TabbedViewModelType> : BaseController<TabbedViewModelType, TabbedViewViewType>
        where EnumType : struct, IConvertible
        where CreatedTabType : TabbedViewTab<CreatedButtonType, EnumType>
        where CreatedButtonType : TabbedViewButton<EnumType>
        where TabbedViewViewType : TabbedViewView<EnumType, CreatedTabType, CreatedButtonType>
        where TabbedViewModelType : TabbedViewModel<EnumType, CreatedTabType, CreatedButtonType, TabbedViewViewType>
    {

    }
}

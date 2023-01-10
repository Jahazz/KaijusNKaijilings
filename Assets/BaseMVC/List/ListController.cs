namespace MVC.List
{
    public abstract class ListController<ElementType, ElementData, ListView, ListModel> : BaseController<ListModel, ListView> 
        where ElementType : ListElement<ElementData> 
        where ListView : BaseView 
        where ListModel : BaseModel<ListView>
    {

    }
}

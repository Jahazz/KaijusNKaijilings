namespace MVC.List
{
    public abstract class ListModel<ElementType, ElementData, ListView> : BaseModel<ListView>
        where ElementType : ListElement<ElementData> 
        where ListView : ListView<ElementType, ElementData>
    {

    }
}


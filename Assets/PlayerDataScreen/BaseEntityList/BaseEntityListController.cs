using MVC.SingleSelectableList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntityListController<ViewType, ModelType> : SingleSelectableListController<BaseEntityListElement, Entity, ViewType, ModelType> where ViewType : BaseEntityListView where ModelType : BaseEntityListModel<ViewType>
{

}

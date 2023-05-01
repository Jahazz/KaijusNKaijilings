using MVC.SingleSelectableList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntityListModel<ViewType> : SingleSelectableListModel<BaseEntityListElement, Entity, ViewType> where ViewType : BaseEntityListView
{

}

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Base
{
    public class CustomListView<DataType> : CustomVisualElement<CustomListView<DataType>>
    { 
        public ObservableCollection<DataType> Collection { get; set; }

        public void Initialize (ObservableCollection<DataType> collection)
        {
            Collection = collection;
        }

        public new class UxmlTraits : CustomUxmlTraits<CustomListView<DataType>>
        {

            public override void Init (VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                InitializeElements(ve, "PlayerDataScreen/CustomSlider", out CustomListView<DataType> createdListView, out VisualElement visualElement);

                //createdListView.Collection.CollectionChanged += HandleCollectionChanged;
            }

            private void HandleCollectionChanged (object sender, NotifyCollectionChangedEventArgs arguments)
            {

            }
        }
    }
}


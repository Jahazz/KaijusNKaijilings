using UnityEngine;
using Utils;

namespace MVC
{
    public class BaseController<ModelType, ViewType> : MonobehaviourWithEvents where ModelType : BaseModel<ViewType> where ViewType : BaseView
    {
        [field: SerializeField]
        public ModelType CurrentModel { get; private set; }

        [field: SerializeField]
        public ViewType CurrentView { get; private set; }

        protected virtual void Awake ()
        {
            CurrentModel.Initialize(CurrentView);
            base.Start();
        }
    }
}

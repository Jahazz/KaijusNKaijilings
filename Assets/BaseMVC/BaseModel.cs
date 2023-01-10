using Utils;

namespace MVC
{
    public class BaseModel<ViewType> : MonobehaviourWithEvents where ViewType : BaseView
    {
        public ViewType CurrentView { get; private set; }

        public virtual void Initialize (ViewType currentView)
        {
            CurrentView = currentView;
        }
    }
}

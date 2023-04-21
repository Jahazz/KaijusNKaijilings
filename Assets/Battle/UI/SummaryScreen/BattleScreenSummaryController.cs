using MVC;

namespace BattleCore.UI.SummaryScreen
{
    public class BattleScreenSummaryController : BaseController<BattleScreenSummaryModel, BattleScreenSummaryView>
    {
        public void OpenScreen (BattleResultType battleResult)
        {
            CurrentModel.OpenScreen(battleResult);
        }

        public void CloseScreen ()
        {
            CurrentModel.CloseScreen();
        }
    }
}

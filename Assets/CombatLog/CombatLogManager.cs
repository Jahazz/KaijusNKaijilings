using BattleCore;
using UnityEngine;

public class CombatLogManager : MonoBehaviour
{

    public void Initialize (Battle currentBattle)
    {
        foreach (var item in currentBattle.BattleParticipantsCollection)
        {
            item.CurrentEntity.OnVariableChange += HandleOnEntityChange;
        }
    }

    private void HandleOnEntityChange (Entity newValue, Entity oldValue)
    {
        throw new System.NotImplementedException();
    }
}

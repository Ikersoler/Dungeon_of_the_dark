using UnityEngine;

public class UIManager : MonoBehaviour
{
    public CombatSystem combatSystem;

    public void OnAttackButton()
    {
        combatSystem.Attack();
    }

    public void OnFleeButton()
    {
        combatSystem.Flee();
    }
}
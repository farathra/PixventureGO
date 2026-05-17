using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public void GiveDamage()
    {
        player.GiveDamage();
    }
    
    private void AttackStarted()
    {
        player.EnableMoveNJump(false);
    }

    private void AttackFinished()
    {
        player.EnableMoveNJump(true);
    }
}

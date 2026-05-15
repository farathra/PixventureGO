using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
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

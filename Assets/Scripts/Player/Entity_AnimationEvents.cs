using UnityEngine;

public class Entity_AnimationEvents : MonoBehaviour
{
    private Entity entity;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    public void GiveDamage()
    {
        entity.GiveDamage();
    }
    
    private void AttackStarted()
    {
        entity.EnableMoveNJump(false);
    }

    private void AttackFinished()
    {
        entity.EnableMoveNJump(true);
    }
}

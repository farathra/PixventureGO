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
        entity.EnableMove(false);
    }

    private void AttackFinished()
    {
        entity.EnableMove(true);
    }
}

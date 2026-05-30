using UnityEngine;

public class Enemy_Goblin : Enemy
{

    protected override void Attack()
    {
        base.Attack();
        StealMoney();
    }

    private void StealMoney()
    {
        Debug.Log(enemyName + " steals money");
    }
}

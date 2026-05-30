using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected string enemyName;

    private void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }
    }

    private void Move()
    {
        Debug.Log(enemyName + " moves at speed " + moveSpeed);
    }

    protected virtual void Attack()
    {
        Debug.Log(enemyName + " attacks");
    }

    public void TakeDamage()
    {

    }

    public string GetEnemyName()
    { 
        return enemyName;
    }
}


using UnityEngine;

public class Enemy : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    [SerializeField] private float damageColorDuration = 2;
    public float currentTimeInGame;
    public float lastTimeDamaged;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        currentTimeInGame = Time.time;

        turnWhite();
    }
    
    public void TakeDamage()
    {
        spriteRenderer.color = Color.red;
        lastTimeDamaged = Time.time;
    }

    private void turnWhite()
    {
        if (currentTimeInGame > lastTimeDamaged + damageColorDuration && spriteRenderer.color != Color.white) 
        {    
            spriteRenderer.color = Color.white;
        }
    }
}

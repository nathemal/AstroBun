using UnityEngine;

public class ChangeEnemyColor : MonoBehaviour
{
    private SpriteRenderer enemyBody;
    public Color HalfHealthColor; 
    public Color LowHealthColor; 
    private EnemyHealthController enemyHealth;
 
    private void Start()
    {
        enemyBody = GetComponent<SpriteRenderer>();

        enemyHealth = GetComponent<EnemyHealthController>();
    }

    public void ChangeSpriteColor()
    {
        float healthPRoc = enemyHealth.CalculatehealthProcentage();

        if (healthPRoc <= 50 && healthPRoc > 30)
        {
            enemyBody.color = HalfHealthColor;
        }

        if (healthPRoc <= 15 && healthPRoc != 0)
        {
            enemyBody.color = LowHealthColor;
        }
    }
}
using UnityEngine;

public class ActivateEnemyDeath : MonoBehaviour
{
    public Material glowMaterial;
    public Color soulColor;
    public SpriteRenderer enemySilhouette;
    //public GameObject enemySilhouette;
    private EnemyHealthController enemy;
    public float speed;
   
    void Start()
    {
        enemy = GetComponent<EnemyHealthController>();
       // enemySilhouette = GetComponent<SpriteRenderer>();

        enemySilhouette.color = soulColor;
        enemySilhouette.material = glowMaterial;

        enemySilhouette.enabled = false;
    }

    // Update is called once per frame

    public void SetSoulActive()
    {
        transform.position = enemy.transform.position;
        enemySilhouette.enabled = true;
        AnimateSoulMovement();
    }

    private void AnimateSoulMovement()
    {
        //transform.position += new Vector3(0, 0, 1 * Time.deltaTime);
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}

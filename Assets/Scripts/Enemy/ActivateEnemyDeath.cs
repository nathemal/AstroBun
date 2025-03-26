using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemyDeath : MonoBehaviour
{
    public GameObject enemySilhouette;
    private GameObject instantaitedSoul;
    private EnemyHealthController enemy;
    public float speed;
    public float souExistanceDuration;
   
    void Start()
    {
        enemy = GetComponent<EnemyHealthController>();
    }

    public void SetSoulActive()
    {
        transform.position = enemy.transform.position;
        instantaitedSoul = Instantiate(enemySilhouette, enemy.transform.position, Quaternion.identity);

        StartCoroutine(AnimateSoulEmissionIntensity(instantaitedSoul.transform, souExistanceDuration));
    }

    private IEnumerator AnimateSoulEmissionIntensity(Transform enemySoulTransform, float duration)
    {
        float timeHasPassed = 0.0f;
        SpriteRenderer sr = enemySoulTransform.GetComponent<SpriteRenderer>();

        Material soulMaterial = sr.material;
        Color initialEmissionColor = soulMaterial.GetColor("_EmissionColor");

        while (timeHasPassed < duration)
        {
            float fadeAmount = Mathf.Lerp(1f, 0f, timeHasPassed / duration);
            soulMaterial.SetColor("_EmissionColor", initialEmissionColor * fadeAmount);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, fadeAmount);
            
            timeHasPassed += Time.deltaTime;
            yield return null;
        }

        Destroy(enemySoulTransform.gameObject);
    }


    
}

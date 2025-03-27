using UnityEngine;

public class LootFollowPlayer : MonoBehaviour
{
    [HideInInspector] public Transform targetToMoveTowards;
    public int lootMovementSpeed;


    private void Start()
    {
        GameObject actualPlayer = GameObject.FindGameObjectWithTag("Player");

        if (actualPlayer != null)
        {
            targetToMoveTowards = actualPlayer.transform;
        }

    }

    private void Update()
    {
        if(targetToMoveTowards != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetToMoveTowards.position, lootMovementSpeed * Time.deltaTime);
            
        }
    }
}

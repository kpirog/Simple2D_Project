using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask playerLayerMask;

    private Vector2 raycastDirection;
    public void EnableWeaponRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, raycastDistance, playerLayerMask);

        Debug.DrawRay(transform.position, raycastDirection * raycastDistance, Color.red);

        if (hit)
        {
            Debug.Log(hit.transform.name);
            EventManager.OnPlayerHit();
        }
    }
    public void SetRaycastDirection(bool isLeft)
    {
        raycastDirection = isLeft ? Vector2.left : Vector2.right;
    }
}

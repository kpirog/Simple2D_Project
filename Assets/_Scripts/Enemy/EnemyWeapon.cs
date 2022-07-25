using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private BoxCollider2D weaponCollider;

    public void EnableWeaponCollider()
    {
        if(!weaponCollider.enabled)
            weaponCollider.enabled = true;
    }
    public void DisableWeaponCollider()
    {
        if (weaponCollider.enabled)
            weaponCollider.enabled = false;
    }
    public void SetColliderDirection(bool isLeft)
    {
        Vector3 colliderPos = weaponCollider.offset;

        if ((isLeft && colliderPos.x > 0f) || (!isLeft && colliderPos.x < 0f))
        {
            colliderPos.x *= -1f;
        }
        else
        {
            return;
        }

        weaponCollider.offset = colliderPos;
    }
}

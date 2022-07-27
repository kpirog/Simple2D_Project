using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GetItem();
        }
    }
    protected abstract void GetItem();
}

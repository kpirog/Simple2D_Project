using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        EventManager.OnRoundComplete += EventManager_OnRoundComplete;
    }
    protected virtual void OnDisable()
    {
        EventManager.OnRoundComplete -= EventManager_OnRoundComplete;
    }
    protected virtual void EventManager_OnRoundComplete(bool obj)
    {
        Destroy(gameObject);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GetItem();
        }
    }
    protected abstract void GetItem();
}

using UnityEngine;

public class ProximityInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private int priority;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Interactor interactor = other.GetComponent<Interactor>();
        if (interactor == null)
            return;

        interactor.AddPossibleInteractable(this, this.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Interactor interactor = other.GetComponent<Interactor>();
        if (interactor == null)
            return;

        interactor.RemovePossibleInteractable(this);
        SetHintVisible(false);
    }

    public virtual bool CanBeInteractedWith()
    {
        return true;
    }

    public virtual int GetPriority()
    {
        return priority;
    }

    public virtual void Interact(Interactor interaction)
    {

    }

    public virtual void SetHintVisible(bool visible)
    {
        spriteRenderer.color = visible ? Color.cyan : Color.white;
    }

}

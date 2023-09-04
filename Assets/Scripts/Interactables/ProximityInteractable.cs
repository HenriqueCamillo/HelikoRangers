using UnityEngine;

public class ProximityInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private int priority;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player == null)
            return;

        player.AddPossibleInteractable(this, this.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player == null)
            return;

        player.RemovePossibleInteractable(this);
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

    public virtual void Interact(GameObject interactor)
    {

    }

    public virtual void SetHintVisible(bool visible)
    {

    }

}

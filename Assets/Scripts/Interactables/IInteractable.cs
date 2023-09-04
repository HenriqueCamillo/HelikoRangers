using UnityEngine;

public interface IInteractable
{
    void Interact(GameObject interactor);
    int GetPriority();
    bool CanBeInteractedWith();
    void SetHintVisible(bool visible);

}

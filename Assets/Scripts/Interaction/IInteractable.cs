public interface IInteractable
{
    void Interact(Interactor interactor);
    int GetPriority();
    bool CanBeInteractedWith();
    void SetHintVisible(bool visible);

}

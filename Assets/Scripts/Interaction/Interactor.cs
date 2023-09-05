using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Interactor : MonoBehaviour
{
    // TODO: Add types of interactions, to be able to distiguish which interactables enemies can interact to
    private Dictionary<IInteractable, GameObject> possibleinteractables = new Dictionary<IInteractable, GameObject>();
    private List<IInteractable> interactablePriorityQueue = new List<IInteractable>();

    private void Update()
    {
        RefreshInteractablePriorityQueue();
    }

    public void Interact()
    {
        if (interactablePriorityQueue.Count == 0) 
            return;

        interactablePriorityQueue[0].Interact(this);
    }

    public void AddPossibleInteractable(IInteractable interactable, GameObject interactableObject)
    {
        if (possibleinteractables.ContainsKey(interactable))
            return;

        possibleinteractables.Add(interactable, interactableObject);
        UpdateInteractablePriorityQueue(interactable, true);
    }

    public void RemovePossibleInteractable(IInteractable interactable)
    {
        if (!possibleinteractables.ContainsKey(interactable))
            return;

        possibleinteractables.Remove(interactable);
        UpdateInteractablePriorityQueue(interactable, false);
    }

    private void RefreshInteractablePriorityQueue()
    {
        if (interactablePriorityQueue.Count == 0)
            return;

        IInteractable activeInteractable = null;
        if (interactablePriorityQueue.Count > 0)
            activeInteractable = interactablePriorityQueue[0];

        interactablePriorityQueue = interactablePriorityQueue.OrderByDescending(interactable => (
            interactable.GetPriority() * 100000.0f -                      // Priority multiplied by big value so that it will always define the order when values are difference
            (possibleinteractables[interactable].transform.position - this.transform.position).sqrMagnitude  // Decreases priority according to distance
        )).ToList();

        if (activeInteractable != null)
            activeInteractable.SetHintVisible(false);
        
        activeInteractable = interactablePriorityQueue[0];
        activeInteractable.SetHintVisible(true);
    }

    private void UpdateInteractablePriorityQueue(IInteractable interactable, bool wasAdded)
    {
        if (wasAdded)
        {
            interactablePriorityQueue.Add(interactable);
        }
        else
        {
            for (int i = 0; i < interactablePriorityQueue.Count; i++)
            {
                if (interactablePriorityQueue[i] == interactable)
                {
                    interactablePriorityQueue.RemoveAt(i);
                    break;
                }
            }
        }

        RefreshInteractablePriorityQueue();
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class CollectableRetriever : MonoBehaviour, IPointerEnterHandler
{
    private Transform playerTransform;

    void Start()
    {
        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            playerTransform = mainCam.transform;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        RetrieveAllItems();
    }

    private void RetrieveAllItems()
    {
        GameObject[] taggedObjects = 
            GameObject.FindGameObjectsWithTag("Collectable");

        foreach (GameObject obj in taggedObjects)
        {
            CollectableItem item = obj.GetComponent<CollectableItem>();

            if (item != null)
            {
                item.AttractTowards(playerTransform.position);
            }
        }
    }
}

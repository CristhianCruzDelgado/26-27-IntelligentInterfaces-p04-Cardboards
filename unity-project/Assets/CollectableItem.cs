using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CollectableItem : MonoBehaviour, 
                               IPointerEnterHandler, 
                               IPointerExitHandler
{
    private Coroutine attractionRoutine;
    private Transform playerTransform;
    private float speed = 3.0f;
    private bool isBeingAttracted = false;

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
        if (playerTransform == null)
        {
            return;
        }

        if (attractionRoutine != null)
        {
            StopCoroutine(attractionRoutine);
        }

        attractionRoutine = StartCoroutine(MoveTowardsPlayer());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isBeingAttracted)
        {
            return;
        }

        if (attractionRoutine != null)
        {
            StopCoroutine(attractionRoutine);
        }
    }

    private IEnumerator MoveTowardsPlayer()
    {
        Vector3 playerPosition = playerTransform.position;
        
        while (Vector3.Distance(transform.position, playerPosition) > 1.0f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, playerPosition, speed * Time.deltaTime);
            yield return null;
        }

        gameObject.SetActive(false);
    }

    public void AttractTowards(Vector3 targetPosition)
    {
        gameObject.SetActive(true);
        isBeingAttracted = true;

        if (attractionRoutine != null)
        {
            StopCoroutine(attractionRoutine);
        }

        attractionRoutine = StartCoroutine(AttractionRoutine(targetPosition));
    }

    public IEnumerator AttractionRoutine(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        isBeingAttracted = false;
        gameObject.SetActive(false);
    }
}

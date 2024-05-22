using UnityEngine;
using UnityEngine.EventSystems;

public class OrderTrasher : MonoBehaviour, IPointerClickHandler
{
    public AudioSource TrashSound;

    public float Range = 1f;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject playerObj = GameObject.FindWithTag("Player");

        float dist = Vector3.Distance(playerObj.transform.position, transform.position);
        if (dist <= Range && playerObj.GetComponentInChildren<OrderStack>().TryPop(out OrderedItem item))
        {
            Destroy(item.gameObject);

            TrashSound.Play();
        }
    }
}

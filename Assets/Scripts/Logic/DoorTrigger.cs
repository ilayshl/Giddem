using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject controlledDoor;
    private Animator doorTrigger;

    private void Update()
    {
        doorTrigger = controlledDoor.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player here");
            doorTrigger.SetTrigger(doorTrigger.ToString());
        }
    }
}

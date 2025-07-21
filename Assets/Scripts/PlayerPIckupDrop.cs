using UnityEngine;

public class PlayerPickupDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;

    private ObjectGrabable objectGrabable;

    void Update()
    {
        if (objectGrabable == null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                float pickupDistance = 5f;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out ObjectGrabable foundObject))
                    {
                        objectGrabable = foundObject;
                        objectGrabable.Grab(objectGrabPointTransform);
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.E)) // Tambahkan ini agar tekan E bisa drop juga
        {
            objectGrabable.Drop();
            objectGrabable = null;
        }
    }
}

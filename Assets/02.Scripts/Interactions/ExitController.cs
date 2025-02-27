using UnityEngine;

public class ExitController : ObjectIdentifier
{
    public bool isOpen;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerCheck(collision.gameObject.layer))
        {
            DoorOpen();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (LayerCheck(collision.gameObject.layer))
        {
            DoorClose();
        }
    }
    private void DoorOpen()
    {
        isOpen = true;
        SignalManager.Instance.EmitSignal(SignalKey.OpenDoor);
    }
    private void DoorClose()
    {
        isOpen = false;
        SignalManager.Instance.EmitSignal(SignalKey.CloseDoor);
    }
}

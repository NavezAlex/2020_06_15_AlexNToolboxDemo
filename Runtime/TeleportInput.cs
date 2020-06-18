using UnityEngine;

public class TeleportInput : MonoBehaviour
{
    public Teleportation m_targetToInteractWith;
    public KeyCode m_keyToUse = KeyCode.T;

    void Update()
    {
        if (Input.GetKeyDown(m_keyToUse))
        {
            m_targetToInteractWith.StartTeleport();
        }
        if (Input.GetKeyUp(m_keyToUse))
        {
            m_targetToInteractWith.StopTeleport();
        }

    }

}

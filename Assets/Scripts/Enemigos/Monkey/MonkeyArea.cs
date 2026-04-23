
using UnityEngine;

public class MonkeyArea : MonoBehaviour
{
    private bool detected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if(player != null) detected = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if (player != null && detected) detected = false;
    }
    public bool PlayerInArea()
    {
        return detected;
    }
} // class MonkeyArea 
// namespace

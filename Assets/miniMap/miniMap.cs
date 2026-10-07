using UnityEngine;

public class miniMap : MonoBehaviour
{
    public float heightAbovePlayer;
    public Transform player;

    private void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y + heightAbovePlayer, player.position.z);
        
    }

}

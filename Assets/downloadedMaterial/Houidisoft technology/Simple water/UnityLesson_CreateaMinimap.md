Create a camera
Name it Minimap
Drag it above the player
Set it's Projection to Orthographic
Please note the Output Texture slot in hte Output tab
Now, we are going to create a renderTexture
Create > Rendering > RenderTexture
Call it Minimap_renderTExture
Drag the Minimap_renderTexture over teh Output Texture of hte Minimap cam
In the Hierarchy view, rightclick > Create > UICanvas > RawImage
Select the Canvas object and go to 2d view, Hit 'f' for focus
Shift click top left corner of screen (this also sets the pivot point)
Offset it from the top and left border by typing in values x: 20 y:-20
Change Height, width  to be 150 and 150.
Duplicate this and drag it above minimap. Rename it background
Chose a Dark colour
Change Height to be 160, Width: 160
Offset it from the top and left border by typing in values x: 15 y:-15

We can parent minimap camera to the player and it will inherit its position and rotation.
However, many minimaps don't rotate when the player is changing position. 
Here is a small script that changes updates the position but not the rotation.

------
miniMap.cs

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
----------


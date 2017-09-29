using UnityEngine;
using System.Collections;

public class SetFocusPointForVideoCapture : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var normal = -Camera.main.transform.forward;     // Normally the normal is best set to be the opposite of the main camera's forward vector
                                                         // If the content is actually all on a plane (like text), set the normal to the normal of the plane
                                                         // and ensure the user does not pass through the plane
        var position = this.transform.position;
        UnityEngine.VR.WSA.HolographicSettings.SetFocusPointForFrame(position, normal);
    }
}

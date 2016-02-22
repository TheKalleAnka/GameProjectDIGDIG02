using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour {

	public void ChangeScene(string name)
    {
        Application.LoadLevel(name);
    }
}

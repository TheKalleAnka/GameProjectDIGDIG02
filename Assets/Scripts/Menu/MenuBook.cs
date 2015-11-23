using UnityEngine;
using System.Collections;

public class MenuBook : MonoBehaviour {

    Transform[] pageObjects = new Transform[10];

    void Start()
    {
        int counter = 0;
        while ((pageObjects[counter] = transform.GetChild(counter)) != null) { counter++; }
    }

    public void ChangePage(string name)
    {
        for (int i = 0; i < pageObjects.Length; i++ )
        {
            if(pageObjects[i].name == name)
            {
                for(int j = 0; j < pageObjects.Length; j++)
                {
                    pageObjects[j].gameObject.SetActive(false);
                }

                pageObjects[i].gameObject.SetActive(true);
                return;
            }
        }

        Debug.LogError("Could not find page called " + name);
    }
}

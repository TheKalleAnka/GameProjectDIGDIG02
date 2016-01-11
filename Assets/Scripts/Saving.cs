using UnityEngine;
using System.Collections;
using System.Xml;

public class Saving : MonoBehaviour {

    public int spriteId_1;
    string spriteID_1;

    float timer;

	// Use this for initialization
	void Start () 
    {
        timer = Time.time + 5;
	}
	
	// Update is called once per frame
	void Update () 
    {
        spriteID_1 = spriteId_1.ToString();
        Timer();
	}

    void Save()
    {
        XmlDocument xmlDoc = new XmlDocument();
        XmlNode rootNode = xmlDoc.CreateElement("Sprite-Id");
        xmlDoc.AppendChild(rootNode);

        XmlNode bodyPart = xmlDoc.CreateElement("Bodypart");
        XmlAttribute spriteId = xmlDoc.CreateAttribute("Id");
        spriteId.Value = spriteID_1;
        bodyPart.Attributes.Append(spriteId);
        rootNode.AppendChild(bodyPart);
        Debug.Log(spriteID_1);

        xmlDoc.Save("Save.xml");
    }

    public void Timer()
    {
        if (timer <= Time.time)
        {
            Save();
            timer = Time.time + 5;
        }
    }


}

using UnityEngine;
using System.Collections;
using System.Xml;

public class Saving : MonoBehaviour {

    static public int spriteId_1 = 5;
    static string spriteID_1;

    

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        Save();
        
	}

    static void Save()
    {
        spriteID_1 = spriteId_1.ToString();

        XmlDocument xmlDoc = new XmlDocument();
        XmlNode rootNode = xmlDoc.CreateElement("Sprite-Id");
        xmlDoc.AppendChild(rootNode);

        XmlNode node1 = xmlDoc.CreateElement("Bodyparts");
        rootNode.AppendChild(node1);

        XmlNode nodeHead = xmlDoc.CreateElement("Head");
        node1.AppendChild(nodeHead);

        XmlNode bodyparts = xmlDoc.CreateElement("Sprite-Id");
        XmlAttribute spriteId = xmlDoc.CreateAttribute("Id");
        spriteId.Value = spriteID_1;
        bodyparts.Attributes.Append(spriteId);
        nodeHead.AppendChild(bodyparts);
        Debug.Log(spriteID_1);

        xmlDoc.Save("Save.xml");
    }

    public static void Load()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("Save.xml");
        foreach (XmlNode xmlNode in xmlDoc.DocumentElement.ChildNodes[2].ChildNodes[0].ChildNodes)
        {
            Debug.Log(xmlNode.Attributes["Id"].Value);
        }
    }


}

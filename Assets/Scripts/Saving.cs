using UnityEngine;
using System.Collections;
using System.Xml;

public class Saving : MonoBehaviour {

    public static int torsoID_1 = 2;
    static string TorsoID_1;

    public static int headID_1 = 1;
    static string HeadID_1;

    public static int legID_1;
    static string LegID_1;

    

	// Use this for initialization
	void Start () 
    {
        legID_1 = Random.Range(1, 3);
	}
	
	// Update is called once per frame
	void Update () 
    {
        Save();
        
        
	}

    static void Save()
    {
        TorsoID_1 = torsoID_1.ToString();
        HeadID_1 = headID_1.ToString();
        LegID_1 = legID_1.ToString();

        XmlDocument xmlDoc = new XmlDocument();
        XmlNode rootNode = xmlDoc.CreateElement("Sprite-Id");
        xmlDoc.AppendChild(rootNode);

        XmlNode node1 = xmlDoc.CreateElement("Bodyparts");
        rootNode.AppendChild(node1);

        XmlNode nodeHead = xmlDoc.CreateElement("Head");
        XmlAttribute headID = xmlDoc.CreateAttribute("ID");
        headID.Value = HeadID_1;
        nodeHead.Attributes.Append(headID);
        node1.AppendChild(nodeHead);

        XmlNode nodeTorso = xmlDoc.CreateElement("Torso");
        XmlAttribute torsoID = xmlDoc.CreateAttribute("ID");
        torsoID.Value = TorsoID_1;
        nodeTorso.Attributes.Append(torsoID);
        node1.AppendChild(nodeTorso);

        XmlNode nodeLegs = xmlDoc.CreateElement("Legs");
        XmlAttribute legID = xmlDoc.CreateAttribute("ID");
        legID.Value = LegID_1;
        nodeLegs.Attributes.Append(legID);
        node1.AppendChild(nodeLegs);

        Debug.Log(LegID_1);
        


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

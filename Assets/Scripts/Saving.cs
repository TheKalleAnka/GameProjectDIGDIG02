using UnityEngine;
using System.Collections;
using System.Xml;

public class Saving : MonoBehaviour {

    static public int headSprite = 1;
    static public int legsSprite;
    static public int torsoSprite;

    static public int level;

    static public int coins = 100;

    

	void Start () 
    {
        
	}
	
	void Update () 
    {
        Save();
        Load();
	}

    static void Save()
    {

        XmlDocument xmlDoc = new XmlDocument();
        XmlNode rootNode = xmlDoc.CreateElement("Root");
        xmlDoc.AppendChild(rootNode);


        

        XmlNode bodyparts = xmlDoc.CreateElement("Bodyparts");
        XmlAttribute nodeHead = xmlDoc.CreateAttribute("HeadId");
        XmlAttribute nodeTorso = xmlDoc.CreateAttribute("TorsoId");
        XmlAttribute nodeLegs = xmlDoc.CreateAttribute("LegsId");
        nodeLegs.Value = legsSprite.ToString();
        nodeHead.Value = headSprite.ToString();
        nodeTorso.Value = torsoSprite.ToString();
        bodyparts.Attributes.Append(nodeLegs);
        bodyparts.Attributes.Append(nodeTorso);
        bodyparts.Attributes.Append(nodeHead);
        rootNode.AppendChild(bodyparts);

        XmlNode coins = xmlDoc.CreateElement("Coins");
        XmlAttribute nodeCoins = xmlDoc.CreateAttribute("CoinsNum");
        nodeCoins.Value = coins.ToString();
        coins.Attributes.Append(nodeCoins);
        rootNode.AppendChild(coins);

        XmlNode level = xmlDoc.CreateElement("Level");
        XmlAttribute nodeLevel = xmlDoc.CreateAttribute("LevelNum");
        nodeLevel.Value = level.ToString();
        level.Attributes.Append(nodeLevel);
        rootNode.AppendChild(level);

        xmlDoc.Save("Save.xml");
    }
    public static void Load()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("Save.xml");
        XmlNodeList idNodes = xmlDoc.SelectNodes("//Root/Bodyparts");
        XmlNodeList coinNodes = xmlDoc.SelectNodes("//Root/Coins");
        XmlNodeList levelNodes = xmlDoc.SelectNodes("//Root/Level");

        foreach (XmlNode node1 in idNodes)
        {
            int head = int.Parse(node1.Attributes["HeadId"].Value);
            int torso = int.Parse(node1.Attributes["TorsoId"].Value);
            int legs = int.Parse(node1.Attributes["LegsId"].Value);

            Debug.Log("Head: " + head);
            Debug.Log("Torso: " + torso);
            Debug.Log("Legs: " + legs);

        }
        foreach (XmlNode node2 in coinNodes)
        {
            int coin = int.Parse(node2.Attributes["CoinsNum"].Value);

            Debug.Log("Coins: " + coin);
        }
        foreach (XmlNode node3 in levelNodes)
        {
            int level = int.Parse(node3.Attributes["LevelNum"].Value);

            Debug.Log("Level: " + level);
        }
        
    }
}

using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class Saving : MonoBehaviour {

    static public int Level;

    static public int Coins = 100;

    static public void Save(string id, int headId, int torsoId, int armId, int legId)
    {
        XmlDocument xmlDoc = new XmlDocument();
        XmlNode rootNode = xmlDoc.CreateElement("Root");
        xmlDoc.AppendChild(rootNode);

        XmlNode bodyparts = xmlDoc.CreateElement("Bodyparts");
        XmlAttribute nodeHead = xmlDoc.CreateAttribute("HeadId");
        XmlAttribute nodeTorso = xmlDoc.CreateAttribute("TorsoId");
        XmlAttribute nodeLegs = xmlDoc.CreateAttribute("LegsId");
        nodeLegs.Value = legId.ToString();
        nodeHead.Value = headId.ToString();
        nodeTorso.Value = torsoId.ToString();
        bodyparts.Attributes.Append(nodeLegs);
        bodyparts.Attributes.Append(nodeTorso);
        bodyparts.Attributes.Append(nodeHead);
        rootNode.AppendChild(bodyparts);

        XmlNode coins = xmlDoc.CreateElement("Coins");
        XmlAttribute nodeCoins = xmlDoc.CreateAttribute("CoinsNum");
        nodeCoins.Value = Coins.ToString();
        coins.Attributes.Append(nodeCoins);
        rootNode.AppendChild(coins);

        XmlNode level = xmlDoc.CreateElement("Level");
        XmlAttribute nodeLevel = xmlDoc.CreateAttribute("LevelNum");
        nodeLevel.Value = Level.ToString();
        level.Attributes.Append(nodeLevel);
        rootNode.AppendChild(level);

        xmlDoc.Save(@"saves\" + id + ".xml");
    }

    public static int[] Load(string id)
    {
        int[] loaded = new int[5];

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(@"saves\" + id + ".xml");
        XmlNodeList idNodes = xmlDoc.SelectNodes("//Root/Bodyparts");
        XmlNodeList coinNodes = xmlDoc.SelectNodes("//Root/Coins");
        XmlNodeList levelNodes = xmlDoc.SelectNodes("//Root/Level");

        foreach (XmlNode node1 in idNodes)
        {
            int head = int.Parse(node1.Attributes["HeadId"].Value);
            int torso = int.Parse(node1.Attributes["TorsoId"].Value);
            int legs = int.Parse(node1.Attributes["LegsId"].Value);

            loaded[0] = head;
            loaded[1] = torso;
            loaded[2] = legs;

            Debug.Log("Head: " + head);
            Debug.Log("Torso: " + torso);
            Debug.Log("Legs: " + legs);

        }
        foreach (XmlNode node2 in coinNodes)
        {
            int coin = int.Parse(node2.Attributes["CoinsNum"].Value);

            loaded[3] = coin;

            Debug.Log("Coins: " + coin);
        }
        foreach (XmlNode node3 in levelNodes)
        {
            int level = int.Parse(node3.Attributes["LevelNum"].Value);

            loaded[4] = level;

            Debug.Log("Level: " + level);
        }

        return loaded;
    }

    public static string[] GetAllCharacterNames()
    {
        return Directory.GetFiles(@"saves\");
    }
}

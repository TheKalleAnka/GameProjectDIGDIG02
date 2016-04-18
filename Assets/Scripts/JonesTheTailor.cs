using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class JonesTheTailor : MonoBehaviour {

    string[] allNames;
    bool showCharacters = true;
    Vector2 scrollPosition = Vector2.zero;

    public GameObject buttonNext;
    public GameObject buttonPrevious;
    public GameObject buttonConfirm;

    void Start()
    {
        allNames = Saving.GetAllCharacterNames();
    }

    void OnGUI()
    {
        if(showCharacters)
        {
            GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height));

            for(int i = 0; i < allNames.Length; i++)
            {
                if (GUILayout.Button(Path.GetFileNameWithoutExtension(allNames[i])))
                {
                    CreateCharacter(Path.GetFileNameWithoutExtension(allNames[i]));
                    showCharacters = false;
                    print("Created: " + Path.GetFileNameWithoutExtension(allNames[i]));
                }
            }

            GUILayout.EndScrollView();
        }
    }

    public void CreateCharacter(string name)
    {
        GameObject g = CreateCharacterFromSave(name);

        g.transform.GetChild(0).gameObject.SetActive(false);//Deactivate the camera
        g.GetComponent<Rigidbody2D>().isKinematic = true;
        g.transform.SetParent(GameObject.Find("PlayerHolder").transform, true);

        buttonNext.SetActive(true);
        buttonPrevious.SetActive(true);
        buttonConfirm.SetActive(true);

        buttonNext.GetComponent<Button>().onClick.AddListener(g.transform.FindChild("Bobo").GetComponent<BodyPart>().ChooseNextSprite);
        buttonPrevious.GetComponent<Button>().onClick.AddListener(g.transform.FindChild("Bobo").GetComponent<BodyPart>().ChoosePreviousSprite);
    }

    public void SaveCurrentCharacter()
    {
        PlayerValueController character = GameObject.Find("PlayerHolder").transform.GetChild(0).GetComponent<PlayerValueController>();
        Saving.Save(character.name, character.headPart.GetActiveSprite(), character.torsoPart.GetActiveSprite(), character.rightArmPart.GetActiveSprite(), character.rightLowerLegPart.GetActiveSprite());
    }

    GameObject CreateCharacterFromSave(string name)
    {
        int[] saved = Saving.Load(name);

        GameObject obj = (GameObject)Instantiate(Resources.Load("PlayerBob"), Vector3.zero, Quaternion.identity);

        PlayerValueController controller = obj.GetComponent<PlayerValueController>();
        controller.SetPartsValues("Hey",0,0,saved[1],0,0);
        controller.coins = 0;

        return obj;
    }
}
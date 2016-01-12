using UnityEngine;
using UnityEngine.UI;

public class JonesTheTailor : MonoBehaviour {

    [SerializeField]
    GameObject[] stage1;

    [SerializeField]
    GameObject[] stage2;

    [SerializeField]
    GameObject[] stage3;

    [SerializeField]
    GameObject[] stage4;

    [SerializeField]
    GameObject[] stage5;

    [SerializeField]
    GameObject[] stage6;

	public void EnableStage(int num)
    {
        DisableAllStages();

        switch(num)
        {
            case 1:
                for (int i = 0; i < stage1.Length; i++ )
                {
                    stage1[i].SetActive(true);
                }
                    break;
            case 2:
                for (int i = 0; i < stage2.Length; i++)
                {
                    stage2[i].SetActive(true);
                }
                break;
            case 3:
                for (int i = 0; i < stage3.Length; i++)
                {
                    stage3[i].SetActive(true);
                }
                break;
            case 4:
                for (int i = 0; i < stage4.Length; i++)
                {
                    stage4[i].SetActive(true);
                }
                break;
            case 5:
                for (int i = 0; i < stage5.Length; i++)
                {
                    stage5[i].SetActive(true);
                }
                break;
            case 6:
                for (int i = 0; i < stage6.Length; i++)
                {
                    stage6[i].SetActive(true);
                }
                break;
        }
    }

    public void CreateCharacter(bool isBob)
    {
        GameObject g;

        if(isBob)
            g = (GameObject)GameObject.Instantiate(Resources.Load("PlayerBob"), Vector3.zero, Quaternion.identity);
        else
            g = (GameObject)GameObject.Instantiate(Resources.Load("PlayerLina"), Vector3.zero, Quaternion.identity);

        g.transform.GetChild(0).gameObject.SetActive(false);//Deactivate the camera
        g.GetComponent<Rigidbody2D>().isKinematic = true;
        g.transform.SetParent(GameObject.Find("PlayerHolder").transform, true);

        GameObject.Find("ButtonNextSprite").GetComponent<Button>().onClick.AddListener(g.transform.FindChild("Torso").GetComponent<BodyPart>().ChooseNextSprite);
        GameObject.Find("ButtonPreviousSprite").GetComponent<Button>().onClick.AddListener(g.transform.FindChild("Torso").GetComponent<BodyPart>().ChoosePreviousSprite);
    }

    void DisableAllStages()
    {
        for (int i = 0; i < stage1.Length; i++ )
        {
            stage1[i].SetActive(false);
        }
        for (int i = 0; i < stage2.Length; i++)
        {
            stage2[i].SetActive(false);
        }
        for (int i = 0; i < stage3.Length; i++)
        {
            stage3[i].SetActive(false);
        }
        for (int i = 0; i < stage4.Length; i++)
        {
            stage4[i].SetActive(false);
        }
        for (int i = 0; i < stage5.Length; i++)
        {
            stage5[i].SetActive(false);
        }
        for (int i = 0; i < stage6.Length; i++)
        {
            stage6[i].SetActive(false);
        }
    }
}

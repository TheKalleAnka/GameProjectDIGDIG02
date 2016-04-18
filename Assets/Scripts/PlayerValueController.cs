using UnityEngine;
using System.Collections;

public class PlayerValueController : MonoBehaviour {

    public string name;
    public int coins;
    public BodyPart headPart;
    public BodyPart torsoPart;
    public BodyPart leftArmPart;
    public BodyPart rightArmPart;
    public BodyPart leftUpperLegPart;
    public BodyPart leftLowerLegPart;
    public BodyPart rightUpperLegPart;
    public BodyPart rightLowerLegPart;

    void Start()
    {/*
        BodyPart[] bodyParts = GetComponentsInChildren<BodyPart>(true);

        for(int i = 0; i < bodyParts.Length; i++)
        {
            switch(bodyParts[i].gameObject.name)
            {
                case "Head":
                    headPart = bodyParts[i];
                    break;
                case "Bobo":
                    torsoPart = bodyParts[i];
                    break;
                case "LeftArm":
                    leftArmPart = bodyParts[i];
                    break;
                case "RightArm":
                    rightArmPart = bodyParts[i];
                    break;
                case "RightLegU":
                    rightUpperLegPart = bodyParts[i];
                    break;
                case "RightLegL":
                    rightLowerLegPart = bodyParts[i];
                    break;
                case "LeftLegU":
                    leftUpperLegPart = bodyParts[i];
                    break;
                case "LeftLegL":
                    leftLowerLegPart = bodyParts[i];
                    break;
            }
        }*/
    }

    public void SetPartsValues(string name, int coins, int headId, int torsoId, int armId, int legId)
    {
        this.name = name;
        this.coins = coins;

        //headPart.SetActiveSprite(headId);
        torsoPart.SetActiveSprite(torsoId);

        //rightArmPart.SetActiveSprite(armId);
        //leftArmPart.SetActiveSprite(armId);

        //rightUpperLegPart.SetActiveSprite(legId);
        //rightLowerLegPart.SetActiveSprite(legId);

        //leftUpperLegPart.SetActiveSprite(legId);
        //leftLowerLegPart.SetActiveSprite(legId);
    }
}

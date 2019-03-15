using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyMinnow.SALSA;

[CreateAssetMenu(fileName = "CharacterPose")]
public class CharacterPoseState : ScriptableObject
{
    public CharactersPoses[] charactersPoses;
    public void SetCustomBlendshape()
    {
        foreach (CharactersPoses characterPoses in charactersPoses)
        {
            GameObject target = GameObject.Find(characterPoses.gameObjectName);
            if (target)
            {
                RandomEyes3D SalsaRandomEyes;
                if (SalsaRandomEyes = target.GetComponent<RandomEyes3D>())
                {
                    foreach (CharactersPoses.Pose Test in characterPoses.Poses)
                    {
                        int i = 0;
                        while (i < SalsaRandomEyes.customShapes.Length)
                        {
                            if (SalsaRandomEyes.customShapes[i].shapeName == Test.shapeName)
                                break;
                            else
                                i++;
                        }
                        if (i < SalsaRandomEyes.customShapes.Length)
                        {
                            SalsaRandomEyes.customShapes[i].overrideOn = true;
                            SalsaRandomEyes.customShapes[i].rangeOfMotion = Test.shapeValue;
                        }
                        else
                            Debug.LogError("Shape not found");
                    }
                }
            }
            else
                Debug.LogError("GameObject not found");
        }
    }

    public void UndoCustomBlendshape()
    {
        foreach (CharactersPoses characterPoses in charactersPoses)
        {
            GameObject target = GameObject.Find(characterPoses.gameObjectName);
            if (target)
            {
                RandomEyes3D SalsaRandomEyes;
                if (SalsaRandomEyes = target.GetComponent<RandomEyes3D>())
                {
                    foreach (CharactersPoses.Pose Test in characterPoses.Poses)
                    {
                        int i = 0;
                        while (SalsaRandomEyes.customShapes[i].shapeName != Test.shapeName && i < SalsaRandomEyes.customShapes.Length)
                            i++;
                        if (i < SalsaRandomEyes.customShapes.Length)
                        {
                            SalsaRandomEyes.customShapes[i].overrideOn = false;
                            SalsaRandomEyes.customShapes[i].rangeOfMotion = 100;
                        }
                        else
                            Debug.LogError("Shape not found");
                    }
                }
                else
                    Debug.LogError(string.Format("RandomEyes3D component not found on target({0})",target.name));
            }
            else
                Debug.LogError("GameObject not found");
        }
    }
}
[System.Serializable]
public class CharactersPoses
{
    public string gameObjectName;
    [System.Serializable]
    public struct Pose
    {
        public string shapeName;
        public float shapeValue;
    }
    public Pose[] Poses;
}
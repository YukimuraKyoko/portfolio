using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorphAnimation
{
    string name;
    
    SkinnedMeshRenderer renderer;
    int blendShapeIdx;
    float startingValue;
    float direction = 1;
    bool updating = false;
    float increment = 15f;
    //Unused float MaxValue = 100f;
    //Unused float MinValue = 0f;
    public MorphAnimation(string n, SkinnedMeshRenderer r)
    {
        name = n;
        renderer = r;
        blendShapeIdx = r.sharedMesh.GetBlendShapeIndex(n);
        startingValue = r.GetBlendShapeWeight(blendShapeIdx);
    }

    public void Trigger()
    {
        updating = true;
        Debug.LogFormat("Triggering facemorph {0} in {1} direction.", name, (direction > 0) ? "positive" : "negative");
    }

    public void Update()
    {
        if (!updating)
            return;

        bool complete = false;
        float newVal = renderer.GetBlendShapeWeight(blendShapeIdx) + (direction * increment);
        if (newVal <= 0)
        {
            newVal = 0;
            complete = true;
        } else if (newVal >= 100f)
        {
            newVal = 100;
            complete = true;
        }
        renderer.SetBlendShapeWeight(blendShapeIdx, newVal);

        if (complete)
        {
            updating = false;
            direction *= -1;
        }
    }
}

/*
 * //Class array approach didn't work
 * //Reason: In the editor script it kept having an error:
 * "Object Reference not set to an instance of an object"
[System.Serializable]
 public class PairElement
{
    public string param;
    public KeyCode key;

    public PairElement()
    {

    }
}
*/

public class AnimationManager : MonoBehaviour
{
    public Animator animator;
    public string[] temp;
    public KeyCode[] keys;
    public string[] parameters;

    public Dictionary<KeyCode, string> animationMap = new Dictionary<KeyCode, string>() {};

    Dictionary<KeyCode, string> morphShapeMap = new Dictionary<KeyCode, string>()
    {
        {KeyCode.J, "Ahegao Yeah"},
        {KeyCode.K, "TongueOut_Ahhh"},
        {KeyCode.H, "Heart Eyes"}
    };

    Dictionary<string, MorphAnimation> morphAnimationMap = new Dictionary<string, MorphAnimation>();

    // Last jacket var
    bool lastJacket = true;

    void Awake()
    {
        
        // check for conflicting keybindings
        var renderer = GameObject.Find("Body").GetComponent<SkinnedMeshRenderer>();
        if (renderer == null)
        {
            Debug.LogError("AnimationManager could not find skinned mesh renderer. Can't trigger face morphs.");
        }
        else
        {
            foreach (string morphName in morphShapeMap.Values)
            {
                morphAnimationMap.Add(morphName, new MorphAnimation(morphName, renderer));
            }
        }
    }

    // Use this for initialization		
    void Start()
    {
        if (animator == null)
        {
            Debug.LogError("No animator found. Is it set in the inspector?");
        }

        //Initialize parameter array into the animationMap on Start
        for (int i = 0; i < parameters.Length; i++)
        {
            animationMap.Add(keys[i], parameters[i]);

            //Debug animationMap
            //Debug.Log("keys["+i+"] = " + keys[i] + " parameters["+i+"] = " + parameters[i]);
        }
    }

    void OnGUI()
    {
        if (animator == null)
        {
            return;
        }
           
        if (animator.parameterCount == 0)
        {
            Debug.LogError("Could not retrieve parameters in the selected Animator!");
        }
       

        

        if (Event.current.isKey && Event.current.type == EventType.KeyDown)
        {
            var pressedKey = Event.current.keyCode;
            if (animationMap.ContainsKey(pressedKey))
            {
                string animationTriggered = animationMap[pressedKey];
                bool currentValue = animator.GetBool(animationTriggered);

                if (animator.runtimeAnimatorController != null)
                {
                    animator.SetBool(animationTriggered, !currentValue);
                }
                    
            } else if (morphShapeMap.ContainsKey(pressedKey))
            {
                string morphTriggered = morphShapeMap[pressedKey];
                morphAnimationMap[morphTriggered].Trigger();
            }
        }

        
    }

    void Update()
    {
        foreach (MorphAnimation ma in morphAnimationMap.Values)
        {
            ma.Update();
        }
    }

    public void SetOnElgatoTriggered()
    {
        //Connecting Elgato
        ElgatoInputSystem.Instance._onTriggerAnimation = OnElgatoTriggered;
    }

    private void OnElgatoTriggered(int keyCode, bool isPressed)
    {
        KeyCode pressedKey = (KeyCode)keyCode;
        if (animationMap.ContainsKey(pressedKey))
        {
            string animationTriggered = animationMap[pressedKey];
            animator.SetBool(animationTriggered, isPressed);
        }
        else if (morphShapeMap.ContainsKey(pressedKey))
        {
            string morphTriggered = morphShapeMap[pressedKey];
            morphAnimationMap[morphTriggered].Trigger();
        }
    }
}

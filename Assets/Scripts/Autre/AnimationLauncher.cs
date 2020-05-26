using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AnimationLauncher : SerializedMonoBehaviour
{
    [Title("Objet to animate")]
    [SerializeField] GameObject objectWithAnimation;
    
    [TitleGroup("Animations Name")]
    [SerializeField] List<String> animationToLaunch = new List<string>();
    [SerializeField] private String animationIdleName = "IDLE";
    
    [TitleGroup("Animations Condition")]
    [SerializeField,DictionaryDrawerSettings(KeyLabel = "Animation Name",ValueLabel = "Condition")] Dictionary<String,LaunchAnimationCondition>  animationConditions = new Dictionary<string, LaunchAnimationCondition>();
    
    [TitleGroup("Additional Parameter")]
    [SerializeField, DictionaryDrawerSettings(KeyLabel = "Animation Name + Condition Name",ValueLabel = "Parameter")] Dictionary<String,float> animationParameters = new Dictionary<string, float>();
    [SerializeField] private bool resetTimer = false;
    //Private Variable
    private Animation animation;
    private List<float> timers = new List<float>();
    private bool hasATimer = false;

    private void Start()
    {
        animation = objectWithAnimation.GetComponent<Animation>();
        if(animation == null) throw new ArgumentNullException(objectWithAnimation.name,"L'objet en paramètre ne contient pas d'animation.");
        /*
        hasATimer = CheckAllAnimation(LaunchAnimationCondition.OnTimerEnd);
        for (int i = 0; i < GetNumberOfAnimation(LaunchAnimationCondition.OnTimerEnd); i++)
        {
            timers.Add(0);
        }
        */
    }
    
    //Logic of the different launcherAnimation
    private void Update()
    {
        if (hasATimer)
        {
            throw new NotImplementedException("La fonction pour les timer n'est pas encore implémenter.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckAllAnimation(LaunchAnimationCondition.OnTriggerEnter);
    }
    private void OnTriggerExit(Collider other)
    {
        CheckAllAnimation(LaunchAnimationCondition.OnTriggerExit);
    }
    private void OnTriggerStay(Collider other)
    {
        CheckAllAnimation(LaunchAnimationCondition.OnTriggerStay);
    }
    private void OnCollisionEnter(Collision other)
    {
        CheckAllAnimation(LaunchAnimationCondition.OnCollisionEnter);
    }
    private void OnCollisionExit(Collision other)
    {
        CheckAllAnimation(LaunchAnimationCondition.OnCollisionExit);
    }
    private void OnCollisionStay(Collision other)
    {
        CheckAllAnimation(LaunchAnimationCondition.OnCollisionStay);
    }

    
    //Methods Used for verifed if we launch an animation;
    private bool CheckAllAnimation(LaunchAnimationCondition conditionToCheck)
    {
        bool found = false;
        bool firstOne = true;
        for (int i = 0; i < animationToLaunch.Count; i++)
        {
            var anim = animationToLaunch[i];
            if (AnimationChecker(anim,conditionToCheck))
            {
                
                if (!firstOne)
                {
                    animation.PlayQueued(anim,QueueMode.CompleteOthers);
                }
                else if(!found)
                {
                    animation.PlayQueued(anim,QueueMode.PlayNow);
                    firstOne = false;
                }
                found = true;
            }
        }
        EndAnimation();
        return found;
    }

    private int GetNumberOfAnimation(LaunchAnimationCondition condition)
    {
        int numberOfAnimation = 0;
        for (int i = 0; i < animationToLaunch.Count; i++)
        {
            var anim = animationToLaunch[i];
            if (AnimationChecker(anim,condition))
            {
                numberOfAnimation++;
            }
        }

        return numberOfAnimation;
    }
    
    private bool AnimationChecker(String animationName, LaunchAnimationCondition conditionToCheck)
    {
        bool found = false;
        foreach (var item in animationConditions)
        {
            if (item.Key == animationName && item.Value == conditionToCheck)
            {
                found = true;
                break;
            }
        }

        return found;
    }
    private void EndAnimation()
    {
        animation.PlayQueued(animationIdleName, QueueMode.CompleteOthers);
    }
}

public enum LaunchAnimationCondition
{
    OnTriggerEnter,
    OnTriggerExit,
    OnTriggerStay,
    OnCollisionEnter,
    OnCollisionExit,
    OnCollisionStay/*,
    OnTimerEnd*/
}

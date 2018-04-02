using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/State")]
public class State : ScriptableObject
{

    public Action[] actions;
    public Transition[] transitions;
    public Color color = Color.grey;
    

    public void StateUpdate(AIController controller)
    {
  
   
        Actions(controller);
        Check(controller);
    }

    private void Actions(AIController controller)
    {
       
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Trigger(controller);
        }
        
    }

    private void Check(AIController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            

            if (transitions[i].decision.Trigger(controller))
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
            {
                controller.TransitionToState(transitions[i].falseState);
            }
        }
    }


}
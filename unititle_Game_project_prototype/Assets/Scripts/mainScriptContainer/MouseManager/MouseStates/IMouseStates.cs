using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMouseStates
{
    //readme
    /*
    this interface gives the classes implementing the 
    interface to have a method to transition to different states

    states can transition to different state during
    each update. etc mouseIdle --> mouseMoveSelectedObject

    The states have different ways of implementing this so do check them out.
    */
    public IMouseStates DoState(MouseBehaviour mouseBehaviour);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMouseStates
{
    //readme
    /*you get the idea, this is done in 
        the update call.

    states can transition to different state during
    each update. etc mouseIdle --> mouseMoveSelectedObject

    The states have different ways of implementing this so do check them out.
    */
    public IMouseStates DoState(MouseBehaviour mouseBehaviour);
}

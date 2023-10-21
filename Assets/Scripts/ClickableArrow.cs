using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClickableArrow : MonoBehaviour
{
    public bool UpArrow;
    public ChannelManager cm;
    
    void OnMouseDown() {
        if(UpArrow) cm.ChannelUp();
        else cm.ChannelDown();
    }
}

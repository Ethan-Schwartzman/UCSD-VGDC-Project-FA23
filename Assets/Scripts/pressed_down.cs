using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pressed_down : MonoBehaviour
{
    Image m_Image;
    Image temp_img;
    //Set this in the Inspector
    public Sprite m_Sprite;
    public Sprite temp_Sprite;

    void Start()
    {
        //Fetch the Image from the GameObject
        m_Image = GetComponent<Image>();
    }

    void Update()
    {

        //Press up to change the Sprite of the Image to pressed_down
        if (Input.GetKey(KeyCode.DownArrow))
        {
            m_Image.sprite = m_Sprite;

            // Vector3 pos=transform.position;
            // pos.y=0;
            transform.localScale=new Vector3(0.6f,0.750f,1);
            // transform.position=pos;
        }
        //after releasing the button, the image reverts back to before it was pressed
        if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            m_Image.sprite = temp_Sprite;

            // Vector3 pos=transform.position;
            // pos.y=0;
           
            transform.localScale=new Vector3(0.5f,0.5f,1);
            // transform.position=pos;
        }
        
    }
}

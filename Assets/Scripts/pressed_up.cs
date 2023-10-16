using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    //m_image is the image we changed to
    Image m_Image;
    //temp_img is the original image
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

        //Press up to change the Sprite of the Image to pressed down
        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_Image.sprite = m_Sprite;
        }
        //after releasing the button, the image reverts back 
        if(Input.GetKeyUp(KeyCode.UpArrow)){
            m_Image.sprite = temp_Sprite;
        }
        
    }
}

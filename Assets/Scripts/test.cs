using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
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

        //Press space to change the Sprite of the Image
        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_Image.sprite = m_Sprite;
        }
        if(Input.GetKeyUp(KeyCode.UpArrow)){
            m_Image.sprite = temp_Sprite;
        }
        
    }
}

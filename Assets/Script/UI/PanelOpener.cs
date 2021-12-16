using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PanelOpener : MonoBehaviour
{
    [SerializeField] GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPanel()
    {
        Animator anim = panel.GetComponent<Animator>();
        
        if (anim != null)
        {
            bool isOpen = anim.GetBool("open");
            anim.SetBool("open", !isOpen);
        }
    }

    public void ClosePopup()
    {
        Animator anim = panel.GetComponent<Animator>();

        if (anim != null)
        {
            bool isOpen = anim.GetBool("open");
            anim.SetBool("open", false);
        }
    }
}

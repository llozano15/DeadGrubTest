using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HouseDescUI : MonoBehaviour
{   
    public GameObject NotePad;
    private Animator anim;
    private bool isOpen = false;

    void Start()
    {
        Time.timeScale = 1;
        anim = NotePad.GetComponent<Animator>();
        anim.enabled = false;
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && !isOpen)
        {
            OpenDesc();
        }
        else if (Input.GetKeyUp(KeyCode.Space) && isOpen)
        {
            CloseDesc();
        }
    }

    //function to open the house description
    public void OpenDesc()
    {
        anim.enabled = true;
        anim.Play("OpenNotepad");
        isOpen = true;
        Time.timeScale = 1;
    }
    
    //function to close the house description
    public void CloseDesc()
    {   
        anim.Play("CloseNotepad");
        isOpen = false;
        Time.timeScale = 1;
    }
} 

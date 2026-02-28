using UnityEngine;

public class HouseDescUI : MonoBehaviour
{
    public GameObject Container;
    private Animator anim;
    private bool isOpen = false;

    void Start()
    {
        Time.timeScale = 1;
        anim = Container.GetComponent<Animator>();
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
        anim.Play("DescSlideIn");
        isOpen = true;
        Time.timeScale = 1;
    }
    //function to close the house description
    public void CloseDesc()
    {
        isOpen = false;
        anim.Play("DescSlideOut");
        Time.timeScale = 1;
    }
}

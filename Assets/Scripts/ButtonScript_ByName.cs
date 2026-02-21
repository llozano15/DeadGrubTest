using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{   
    //Creates variable to store name of scene to be loaded
    //Appears in the Inspector
    public string sceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //We are making a loadscene function
    //That passes the name of our scen into it when we call the function
    //inside the () is called parameters/arguments
    public void LoadScene()
    {   
        //Loads scene with matching name
        SceneManager.LoadScene(sceneName);
    }
}

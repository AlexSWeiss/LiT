using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject exitPanel;
    public GameObject headPanel;

    void Update()
    {
        if (exitPanel.activeSelf == false && Input.GetKeyDown(KeyCode.Escape))
        {
            exitPanel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitPanel.SetActive(false);
        }
    }

    public void OnClickStart()
    {
      if(!headPanel.GetComponent<Animator>().enabled)
        {
            headPanel.GetComponent<Animator>().enabled = true;
        }
      else
        {
            headPanel.GetComponent<Animator>().SetTrigger("In");
        }

        SceneManager.LoadScene(1);

    }

    public void OnClickOpnSet()
    {
        SceneManager.LoadScene(2);
    }

    public void OnClickExitMenu()
    {
        exitPanel.SetActive(true);
    }

    public void OnClickExitN()
    {
        exitPanel.SetActive(false);
    }

    public void OnClickExitY()
    {
        Application.Quit();
    }

}

using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsScript : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void OnClickToMenu()
    {
        SceneManager.LoadScene(0);
    }

}

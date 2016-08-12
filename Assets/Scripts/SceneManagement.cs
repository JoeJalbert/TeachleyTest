using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneManagement {

	public static void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

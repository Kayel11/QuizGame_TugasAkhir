using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMMenu : MonoBehaviour
{
    private static BGMMenu instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "QuizScene")
        {
            Destroy(gameObject);
        }
    }
}
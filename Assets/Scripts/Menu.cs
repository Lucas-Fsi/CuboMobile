using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameManager GameManager;
    public static bool InicioRapido = false;

    private void Start()
    {
        if (InicioRapido == true) GameManager.Enabled();
        GetComponentInChildren<TMPro.TextMeshPro>().gameObject.LeanScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setLoopPingPong();
    }

    public void Play()
    {
        GetComponent<CanvasGroup>().LeanAlpha(0, 0.2f).setOnComplete(IniciaGame);
    }
    public void IniciaGame()
    {
        GameManager.Enabled();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        InicioRapido = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }

    public void Exit()
    {
        Time.timeScale = 1f;
        InicioRapido = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        

    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Starting Game...");
        SceneManager.LoadScene("GameScene");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit(); // 빌드 파일 종료

        // 에디터 테스트 중일 때 종료
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif  
    }
    public void OpenOptions()
    {
        Debug.Log("Opening Options Menu...");
    }

}

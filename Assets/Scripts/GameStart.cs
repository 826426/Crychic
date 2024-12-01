using UnityEngine;
using UnityEngine.SceneManagement; // 用于加载场景
using UnityEngine.UI; // 如果使用 Text

public class GameStart : MonoBehaviour
{
    private string sceneToLoad = "Level_1"; // 游戏主场景的名称

    private bool keyPressed = false;

    void Update()
    {
        // 检测任意键输入
        if (!keyPressed && Input.anyKeyDown)
        {
            keyPressed = true;
            // 可以播放一个动画或过渡效果
            StartGame();
        }
    }

    void StartGame()
    {
        // 加载主场景
        SceneManager.LoadScene(sceneToLoad);
    }
}

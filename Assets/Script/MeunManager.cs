using UnityEngine;
using UnityEngine.SceneManagement;

public class MeunManager : MonoBehaviour
{
    /// <summary>
    /// ���}�C��
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// �}�l�C��
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("�C������");
    }
}

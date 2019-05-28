using UnityEngine;
using UnityEngine.SceneManagement;
namespace Shooter
{
    public class MySceneManager : MonoBehaviour
    {
        public static void LoadSceneS(string nameScene)
        {
            SceneManager.LoadScene(nameScene);
        }
        public void LoadScene(string nameScene)
        {
            SceneManager.LoadScene(nameScene);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinishTrigger : MonoBehaviour
{
    private bool gameEnded = false;

    private void OnTriggerEnter(Collider other)
    {
        if (gameEnded) return;

        if (other.CompareTag("Player"))
        {
            gameEnded = true;
            Debug.Log("🎉 Player reached the finish line!");
            EndGame();
        }
    }

    void EndGame()
    {
        Time.timeScale = 0f;
        Debug.Log("🏆 Game Over! You Win!");
    }
}

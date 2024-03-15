using UnityEngine;
using UnityEngine.SceneManagement;

public class restartblock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Vérifier si le joueur entre en collision avec l'objet spécifique
        {
            RestartScene();
        }
    }

    private void RestartScene()
    {
        // Charger à nouveau la scène actuelle
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}

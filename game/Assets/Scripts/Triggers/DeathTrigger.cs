using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// Триггер смерти игрока
/// </summary>
public class DeathTrigger : MonoBehaviour {
    /// <summary>
    /// Срабатывание триггера при столкновении с ним. Игрок умирает, загружается сцена с текстом о поражении
    /// </summary>
    /// <param name="coll">Коллайдер объекта, с которым произошло столкновение</param>
    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.name == "MainCharacter") {
            SceneManager.LoadScene("YouLost");
        }
    }
}

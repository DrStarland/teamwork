using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// Триггер победы
/// </summary>
public class LevelEndTrigger : MonoBehaviour {
    /// <summary>
    /// Срабатывание триггера при столкновении с ним. Игрок побеждает, загружается сцена с предложением выбрать следующий уровень
    /// </summary>
    /// <param name="coll">Коллайдер объекта, с которым произошло столкновение</param>
    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.name == "MainCharacter") {
            SceneManager.LoadScene("Finish");
        }
    }
}

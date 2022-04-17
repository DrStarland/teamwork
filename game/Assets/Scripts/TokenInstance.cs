using UnityEngine;

[RequireComponent(typeof(Collider2D))]
/// <summary>
/// Этот класс содержит данные, необходимые для реализации механики сбора монеток
/// </summary>
public class TokenInstance : MonoBehaviour {
    /// <summary>
    /// Звук, воспроизводимый при сборе монетки
    /// </summary>
    public AudioClip tokenCollectAudio;
   
    /// <summary>
    /// Уникальный индекс, присваиваемый TokenController-ом    
    /// </summary>
    internal int tokenIndex = -1;

    /// <summary>
    /// Объект TokenController-а 
    /// </summary>
    internal TokenController controller;

    /// <summary>
    /// Текущий кадр анимации (если есть)
    /// </summary>
    internal int frame = 0;

    /// <summary>
    /// Флаг, не собрана ли монетка
    /// </summary>
    internal bool collected = false;

    /// <summary>
    /// Инициализация монетки: переводится флаг SetActive в активное состояние
    /// </summary>
    void Awake() {
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// Срабатывание триггера при столкновении с ним. Игрок собирает монетку
    /// </summary>
    /// <param name="coll">Коллайдер объекта, с которым произошло столкновение</param>
    void OnTriggerEnter2D(Collider2D coll) {
        var player = coll.gameObject.GetComponent<Player>();
        if (player != null) OnPlayerEnter(player);
    }

    /// <summary>
    /// Функция-"обработчик" сбора монетки
    /// </summary>
    /// <param name="player">Объект игрока, собирающего монетку</param>
    void OnPlayerEnter(Player player) {
        AudioSource.PlayClipAtPoint(tokenCollectAudio, transform.position);
        Score.tokenAmount += 1;
        if (collected) return;
        
        frame = 0;
        
        if (controller != null)
            collected = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Скрипт для объектов, выполняющих роль батута
/// </summary>
public class Trampoline : MonoBehaviour {
    /// <summary>
    /// Ускорение
    /// </summary>
    public float upVelocity;

    /// <summary>
    /// Объект игрока
    /// </summary>
    public Player player;

    /// <summary>
    /// Твердое тело игрока
    /// </summary>
    private Rigidbody2D rigidplayer;

    /// <summary>
    /// Инициализация параметров
    /// </summary>
    void Start() {
        rigidplayer = player.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Срабатывание триггера при столкновении с ним. Игрок подпрыгивает высоко вверх
    /// </summary>
    /// <param name="coll">Коллайдер объекта, с которым произошло столкновение</param>
    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "Player") {
            rigidplayer.AddForce(new Vector2(0, upVelocity));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс вариативных коллизий, возникающих между игроком и врагом
/// </summary>
public class PlayerEnemyCollision : MonoBehaviour {
    /// <summary>
    /// Объект игрока
    /// </summary>
    public Player player;

    /// <summary>
    /// Время до разрушения объекта врага для коллизий, связанных с разрушением
    /// </summary>
    public float timerBeforeDestruction;

    /// <summary>
    /// Объект врага
    /// </summary>
    private Enemy enemy;

    /// <summary>
    /// Объект-аниматор, отвечающий за реакции на коллизии
    /// </summary>
    private Animator anim;

    /// <summary>
    /// Инициализация объектов класса; вызывается перед прорисовкой первого фрейма
    /// </summary>
    void Start() {
        enemy = GetComponent<Enemy>();
        anim = enemy.GetComponent<Animator>();
        anim.enabled = false;
    }

	/// <summary>
	/// Срабатывание триггера при столкновении объектов врага и игрока (а именно - если игрок прыгнул на врага)
	/// </summary>
	/// <param name="coll">Коллайдер объекта, с которым произошло столкновение</param>
    void OnTriggerEnter2D(Collider2D coll) {
        bool willHurtEnemy = player._collider2d.bounds.center.y >= enemy._collider2d.bounds.max.y;
        float velocity = player.GetComponent<Rigidbody2D>().velocity.y;
        if (willHurtEnemy && velocity != 0 && coll.gameObject.tag == "Player") {
            if (timerBeforeDestruction <= 0) {
                Destroy(gameObject);
            } 
        }
    }

	/// <summary>
	/// Срабатывание триггера при нахождении в зоне объекта (а именно - срабатывание постепенного разрушения объекта для коллизии с разрушением)
	/// </summary>
	/// <param name="coll">Коллайдер объекта, с которым произошло срабатывание триггера</param>
    void OnTriggerStay2D(Collider2D coll) {
        if (coll.gameObject.tag == "Player") {
                anim.enabled = true;
                timerBeforeDestruction -= Time.deltaTime;
                if (timerBeforeDestruction <= 0) {
                    Destroy(gameObject);
                }
        }
    }
}

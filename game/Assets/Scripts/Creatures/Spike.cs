using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс объекта, наносящего урон (от прикосновения к нему)
/// </summary>
public class Spike : MonoBehaviour {
	/// <summary>
	/// Количество урона, наносимого от прикосновения
	/// </summary>
    public float damage;

	/// <summary>
	/// Величина, влияющая на отталкивание от прикосновения
	/// </summary>
	public float pushBackForce;

	/// <summary>
	/// Интервал нанесения урона
	/// </summary>
	public float damageRate;

	/// <summary>
	/// Время до следующего нанесения урона
	/// </summary>
	private float nextDamage;

	/// <summary>
	/// Инициализация класса
	/// </summary>
	void Start() {
		nextDamage = 0f;
	}

	/// <summary>
	/// Срабатывание триггера при столкновении объектов (с игроком)
	/// </summary>
	/// <param name="coll">Коллайдер объекта, с которым произошло столкновение</param>
	void OnTriggerStay2D(Collider2D coll) {
		if (coll.tag == "Player" && nextDamage < Time.time) {
			PlayerHealth thePlayerHeath = coll.gameObject.GetComponent<PlayerHealth>();
			thePlayerHeath.addDamage(damage);
			nextDamage = Time.time + damageRate;
			pushBack(coll.transform);
		}
	}

	/// <summary>
	/// Функция отталкивания (при pushBackForce > 0)
	/// </summary>
	/// <param name="pushObject">Расположение объекта, с которым произошло столкновение</param>
	void pushBack(Transform pushObject) {
		Vector2 pushDirection = new Vector2(0, (pushObject.position.y - transform.position.y)).normalized;
		pushDirection *= pushBackForce;
		Rigidbody2D pushRB = pushObject.gameObject.GetComponent<Rigidbody2D>();
		pushRB.velocity = Vector2.zero;
		pushRB.AddForce(pushDirection, ForceMode2D.Impulse);
	}
}

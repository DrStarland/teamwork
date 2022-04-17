using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс полезного предмета, восстанавливающего здоровье
/// </summary>
public class HealthPickUp : MonoBehaviour {
	/// <summary>
	/// Количество восстанавливаемого здоровья
	/// </summary>
	public float healthAmount;

    /// <summary>
    /// Срабатывание триггера при столкновении с ним. Игрок восстанавливает количество здоровья, равное healthAmount
    /// </summary>
    /// <param name="coll">Коллайдер объекта, с которым произошло столкновение</param>
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "Player") {
			PlayerHealth playerHealth  = coll.gameObject.GetComponent<PlayerHealth>();
			playerHealth.addHealth(healthAmount);
			Destroy(gameObject);
		}
	}
}

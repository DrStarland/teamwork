using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Класс оружия врага
/// </summary>
public class EnemySword : MonoBehaviour {
	/// <summary>
	/// Коэффициент, влияющий на масштабирование оружия (учитывает размеры врага)
	/// </summary>
	public float _scale;

	/// <summary>
	/// Количество урона, наносимого оружием врага
	/// </summary>
	public float swordDamage;

	/// <summary>
	/// Начальное значение координаты х оружия врага
	/// </summary>
	private float startX;

	/// <summary>
	/// Текущее значение координаты х оружия врага
	/// </summary>
	private float x;

	/// <summary>
	/// Объект (спрайт) оружия врага
	/// </summary>
	private SpriteRenderer sprite;

	/// <summary>
	/// Инициализация оружия врага; вызывается перед прорисовкой первого фрейма
	/// </summary>
	void Start() {
		startX = this.transform.position.x;

		sprite = GetComponent<SpriteRenderer>();
		sprite.sortingLayerName = "BG";

		_scale = GameObject.Find("ShootingEnemy").transform.localScale.x;
		swordDamage = 20;
	}

	/// <summary>
	/// Обновление оружия врага - скорость и отслеживание пройденного расстояния; вызывается один раз за кадр
	/// </summary>
	void Update() {
			if (_scale >= 0) {
				GetComponent<Rigidbody2D>().velocity = new Vector2(-8f, GetComponent<Rigidbody2D>().velocity.y);
			}

			x = this.transform.position.x;
			if (Math.Abs(Math.Abs(startX) - Math.Abs(x)) >= 10) {
				Destroy(gameObject);
			}		
	}

	/// <summary>
	/// Срабатывание триггера при столкновении
	/// </summary>
	/// <param name="coll">Коллайдер объекта, с которым произошло столкновение</param>
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Ground") {
			Destroy(gameObject);
		}
		
		if (coll.gameObject.tag == "Player") {
			PlayerHealth playerHealth = coll.gameObject.GetComponent<PlayerHealth>();
			playerHealth.addDamage(swordDamage);
			Destroy(gameObject);
		}
	}
	
	/// <summary>
	/// Срабатывание триггера при нахождении в зоне объекта
	/// </summary>
	/// <param name="coll">Коллайдер объекта, с которым произошло срабатывание триггера</param>
	void OnTriggerStay2D(Collider2D coll) {
		if (coll.gameObject.tag == "Ground") {
			Destroy(gameObject);
		}

		if (coll.gameObject.tag == "Player") {
				Destroy(gameObject);
				PlayerHealth playerHealth = coll.gameObject.GetComponent<PlayerHealth>();
				playerHealth.addDamage(swordDamage);
		}
	}
}

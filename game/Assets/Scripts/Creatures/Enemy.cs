using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс врага
/// </summary>
public class Enemy : MonoBehaviour {
	/// <summary>
	/// Маска слоя врага
	/// </summary>
	public LayerMask enemyMask;

	/// <summary>
	/// Скорость передвижения врага
	/// </summary>
	public float speed = 1;

	/// <summary>
	/// Флаг наличия у врага оружия
	/// </summary>
	public bool hasSword;

	/// <summary>
	/// Коллайдер врага; учитывается при физических столкновениях
	/// </summary>
	public Collider2D _collider2d;

	/// <summary>
	/// Игровые объекты оружия и его положения относительно врага
	/// </summary>
	public GameObject EnemySword, posSword;

	/// <summary>
	/// Период (интервал) использования оружия (между двумя выстрелами)
	/// </summary>
	public float period = 1.0f;

	/// <summary>
	/// Время до следующего использования оружия врага
	/// </summary>
	private float nextActionTime = 0.0f;

	/// <summary>
	/// Твердое тело врага
	/// </summary>
	private Rigidbody2D _rigidbody;

	/// <summary>
	/// Расположение врага
	/// </summary>
	private Transform _transform;

	/// <summary>
	/// Ширина и высота границ объекта врага
	/// </summary>
	private float _width, _height;

	/// <summary>
    /// Инициализация врага; вызывается перед прорисовкой первого фрейма
    /// </summary>
	void Start() {
		_collider2d = GetComponent<Collider2D>();
		_transform = this.transform;
  		_rigidbody = this.GetComponent<Rigidbody2D>();
		SpriteRenderer _sprite = this.GetComponent<SpriteRenderer>();
		_width = _sprite.bounds.extents.x;
		_height = _sprite.bounds.extents.y;
	}

	/// <summary>
    /// Функция обновления передвижения и (опционально) стрельбы врага; вызывается из таймера, независимого от частоты кадров
    /// </summary>
	void FixedUpdate() {
		Vector2 lineCastPos = _transform.position.toVector2() - _transform.right.toVector2() * _width + Vector2.up * _height;
		Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
  		bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);

		Debug.DrawLine(lineCastPos, lineCastPos - _transform.right.toVector2() * .05f);
  		bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - _transform.right.toVector2() * .05f, enemyMask);

		Shooting();

		if (isBlocked || isGrounded) {
			Vector3 currRot = _transform.eulerAngles;
			currRot.y += 180;
			_transform.eulerAngles = currRot;
		}
		Vector2 _velocity = _rigidbody.velocity;
		_velocity.x = -_transform.right.x * speed;
		_rigidbody.velocity = _velocity;
	}

	/// <summary>
    /// Срабатывание триггера при столкновении
    /// </summary>
	/// <param name="coll">Коллайдер объекта, с которым произошло столкновение</param>
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.name == "fireball") {
			Destroy(gameObject);
		}
	}

	/// <summary>
    /// Стрельба (необходим активный флаг hasSword)
    /// </summary>
	void Shooting() {
		if (hasSword) {
			if (Time.time > nextActionTime) {
				nextActionTime += period;
				Instantiate(EnemySword, posSword.transform.position, posSword.transform.rotation);
			}
		}
	}
}

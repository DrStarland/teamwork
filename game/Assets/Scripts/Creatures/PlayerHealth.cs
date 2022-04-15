using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс здоровья игрока
/// </summary>
public class PlayerHealth : MonoBehaviour {
	/// <summary>
	/// Общее количество здоровья игрока
	/// </summary>
	public float fullHealth;

	/// <summary>
	/// Звук, который воспроизводится, если здоровье игрока уменьшено
	/// </summary>
	public AudioClip playerHurt;

	/// <summary>
	/// Слайдер для отображения здоровья игрока вверху экрана
	/// </summary>
	public Slider heartBar;

	/// <summary>
	/// Текущее количество здоровья игрока
	/// </summary>
	private float currentHealth;

	/// <summary>
	/// Инициализация здоровья игрока; вызывается перед прорисовкой первого фрейма
	/// </summary>
	void Start() {
		currentHealth = fullHealth;
		heartBar.maxValue = fullHealth;
		heartBar.value = fullHealth;
	}

	/// <summary>
	/// Функция обновления; вызывается каждый кадр; проверяет, не опустилось ли здоровье игрока до нуля
	/// </summary>
	void Update() {
		if (currentHealth <= 0) {
			SceneManager.LoadScene("YouLost");
		}
	}

	/// <summary>
	/// Функция сокращения текущего количества здоровья игрока (вследствие нанесения ему урона)
	/// </summary>
	/// <param name="damage">Количество получаемого урона</param>
	public void addDamage(float damage) {
		if (damage <= 0) return;

		currentHealth = currentHealth - damage;
		AudioSource.PlayClipAtPoint(playerHurt, transform.position, 1f);
		heartBar.value = currentHealth;
	}

	/// <summary>
	/// Функция добавления к текущему количеству здоровья игрока некоторой величины (вследствие нахождения полезного предмета)
	/// </summary>
	/// <param name="health">Количество добавляемого здоровья</param>
	public void addHealth(float health) {
		currentHealth += health;
		if (currentHealth > fullHealth) currentHealth = fullHealth;
		heartBar.value = currentHealth;
	}
}

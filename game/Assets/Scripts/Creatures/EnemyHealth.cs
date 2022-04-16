using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс здоровья врага
/// </summary>
public class EnemyHealth : MonoBehaviour {
	/// <summary>
	/// Общее количество здоровья врага; вещественная величина
	/// </summary>
	public float enemyHealth;
	
	/// <summary>
	/// Слайдер для отображения здоровья врага
	/// </summary>
	public Slider enemyHealthBar;
	
	/// <summary>
	/// Флаг, указывающий, будет ли появляться дополнительный полезный предмет после того, как здоровье врага опустится до 0
	/// </summary>
	public bool willDrop;

	/// <summary>
	/// Игровой объект, выступающий в качестве выпадающего полезного предмета (если здоровье врага опустится до 0 и активен флаг willDrop)
	/// </summary>
	public GameObject dropObject;

	/// <summary>
	/// Звук, который воспроизводится, если здоровье врага опустится до 0
	/// </summary>
	public AudioClip deathKnell;

	/// <summary>
	/// Текущее количество здоровья врага; вещественная величина
	/// </summary>
	private float currentHealth;

	/// <summary>
	/// Инициализация здоровья врага; вызывается перед прорисовкой первого фрейма
	/// </summary>
	void Start() {
		currentHealth = enemyHealth;
		enemyHealthBar.maxValue = currentHealth;
		enemyHealthBar.value = currentHealth;
	}

	/// <summary>
	/// Функция сокращения текущего количества здоровья врага (вследствие нанесения урона врагу)
	/// </summary>
	/// <param name="damage">Количество получаемого урона</param>
	public void addDamage(float damage) {
		currentHealth = currentHealth - damage;
		enemyHealthBar.value = currentHealth;
		if (currentHealth <= 0) {
			makeDead();
		}
	}

	/// <summary>
	/// Функция смерти (уничтожения объекта), если здоровье врага опустится до 0
	/// </summary>
	public void makeDead() {
		AudioSource.PlayClipAtPoint(deathKnell, transform.position, 0.5f);
		Destroy(gameObject);
		if (willDrop) {
			Instantiate(dropObject,transform.position, transform.rotation);
		}
	}
}

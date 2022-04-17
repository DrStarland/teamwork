using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Скрипт-счетчик собранных монеток с отображением их количества вверху экрана
/// </summary>
public class Score : MonoBehaviour {
    /// <summary>
    /// Общее количество собранных монеток
    /// </summary>
    public static int tokenAmount;

    /// <summary>
    /// Текстовое поле для отображения количества собранных монеток
    /// </summary>
    private Text textAmount;

    /// <summary>
    /// Инициализация счетчика
    /// </summary>
    void Start() {
        tokenAmount = 0;
        textAmount = GetComponent<Text>();
    }

    /// <summary>
    /// Обновление отображения счетчика
    /// </summary>
    void Update() {
        textAmount.text = tokenAmount.ToString();
    }
}

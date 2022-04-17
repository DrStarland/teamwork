using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Вспомогательный класс преобразований для объектов
/// </summary>
public static class ExtensionMethods {
	/// <summary>
	/// Преобразование типа Vector3 в тип Vector2
	/// </summary>
	/// <param name="vec3">Изначальный 3-вектор, который будет преобразован</param>
	public static Vector2 toVector2(this Vector3 vec3) {
		return new Vector2(vec3.x, vec3.y);
	}
}

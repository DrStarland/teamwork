using UnityEngine;

/// <summary>
/// Класс, обеспечивающий движение камеры за персонажем (слежение)
/// </summary>
public class CameraFollow2D : MonoBehaviour {
	/// <summary>
	/// "Амортизация" камеры - параметр, влияющий на сглаживание движения камеры
	/// </summary>
	public float damping = 1.5f;

	/// <summary>
	/// Смещение камеры от центра
	/// </summary>
	public Vector2 offset = new Vector2(2f, 1f);

	/// <summary>
	/// Флаг направления движения игрока
	/// </summary>
	public bool faceLeft;

	/// <summary>
	/// Расположение игрока
	/// </summary>
	private Transform player;

	/// <summary>
	/// Координата x игрока в предыдущий момент времени. Учитывается при обновлении направления движения
	/// </summary>
	private int lastX;

	/// <summary>
	/// Инициализация камеры; вызывается перед прорисовкой первого фрейма
	/// </summary>
	void Start() {
		offset = new Vector2(Mathf.Abs(offset.x), offset.y);
		FindPlayer(faceLeft);
	}

	/// <summary>
	/// Поиск игрока (осуществляется по тэгу "Player" ) и установка расположения камеры на сцене
	/// </summary>
	/// <param name="playerFaceLeft">Флаг направления движения персонажа</param>
	public void FindPlayer(bool playerFaceLeft) {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		lastX = Mathf.RoundToInt(player.position.x);

		if (playerFaceLeft) {
			transform.position = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
		}
		else {
			transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
		}
	}

	/// <summary>
	/// Обновление камеры; вызывается один раз за кадр
	/// </summary>
	void Update() {
		if (player) {
			int currentX = Mathf.RoundToInt(player.position.x);
			if (currentX > lastX) {
				faceLeft = false;
			} 
			else if (currentX < lastX) {
				faceLeft = true;
            }

			lastX = Mathf.RoundToInt(player.position.x);

			Vector3 target;
			if (faceLeft) {
				target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
			}
			else {
				target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
			}

			// Lerp -- интересная функция: линейная интерполяция с нелинейным движением. происходит так потому,
			// что положение начальной точки интерполяции меняется при каждом обновлении.
			Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
			transform.position = currentPosition;
		}
	}
}

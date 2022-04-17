using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс, отвечающий за переходы между сценами игры
/// </summary>
public class Menu : MonoBehaviour {
	/// <summary>
	/// Загрузка главного меню игры
	/// </summary>
	public void GoToMenu() {
		SceneManager.LoadScene("Menu");
	}
	
	/// <summary>
	/// Загрузка карты первого уровня игры
	/// </summary>
	public void GoToFirstLevel() {
		SceneManager.LoadScene("FirstLevel");
	}

	/// <summary>
	/// Возобновление уровня после паузы
	/// </summary>
	/// <param name="pauseMenu">Объект меню паузы уровня</param>
	public void GoBackToLevel(GameObject pauseMenu) {
		pauseMenu.SetActive(false);
        Time.timeScale = 1;
	}
	
	/// <summary>
	/// Переход в меню выбора уровня
	/// </summary>
	public void SelectLevel() {
		SceneManager.LoadScene("LevelSelection");
	}
	
	/// <summary>
	/// Переход к экрану с информацией об авторах
	/// </summary>
	public void About() {
		SceneManager.LoadScene("AboutAuthors");
	}
	
	/// <summary>
	/// Завершение игры и выход из программы
	/// </summary>
	public void QuitGame() {
		Application.Quit();
	}
}

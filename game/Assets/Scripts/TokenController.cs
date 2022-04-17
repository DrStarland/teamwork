using UnityEngine;

/// <summary>
/// Класс-контроллер для монеток
/// </summary>
public class TokenController : MonoBehaviour {
    /// <summary>
    /// Число кадров в секунду для анимации монеток
    /// </summary>
    public float frameRate = 12;

    /// <summary>
    /// Экземпляры анимированных монеток. Если пусто, будут найдены и загружены во время выполнения
    /// </summary>
    public TokenInstance[] tokens;

    /// <summary>
    /// Время до следующего кадра
    /// </summary>
    private float nextFrameTime = 0;

    /// <summary>
    /// Поиск всех монеток на сцене
    /// </summary>
    void FindAllTokensInScene() {
        tokens = UnityEngine.Object.FindObjectsOfType<TokenInstance>();
    }

    /// <summary>
    /// Если список монеток пуст, поиск на сцене; если найдены - регистрация в контроллере
    /// </summary>
    void Awake() {
        if (tokens.Length == 0)
            FindAllTokensInScene();
        for (var i = 0; i < tokens.Length; i++) {
            tokens[i].tokenIndex = i;
            tokens[i].controller = this;
        }
    }

    /// <summary>
    /// Функция обновления и проверки, не подобрал ли монетку(-и) еще игрок
    /// </summary>
    void Update() {
        if (Time.time - nextFrameTime > (1f / frameRate)) {
            for (var i = 0; i < tokens.Length; i++) {
                var token = tokens[i];
                if (token != null) {
                    if (token.collected) {
                        token.gameObject.SetActive(false);
                        tokens[i] = null;
                    }
                }
            }
            nextFrameTime += 1f / frameRate;
        }
    }
}

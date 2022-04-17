using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Класс игрока
/// </summary>
public class Player : Creature {
    /// <summary>
    /// Коллайдер игрока; учитывается при физических столкновениях
    /// </summary>
    public Collider2D _collider2d;

    /// <summary>
    /// Игровые объекты оружия и его положения относительно игрока; меню паузы
    /// </summary>
    public GameObject Sword, posSword, pauseMenu;

    /// <summary>
    /// Объект, отвечающий за анимацию для игрока
    /// </summary>
    public Animator anim;

    /// <summary>
    /// Спрайт "барьера" игрока
    /// </summary>
    private SpriteRenderer posSprite;

    /// <summary>
    /// Временная величина до следующего выстрела
    /// </summary>
    private float nextFire = 0;

    /// <summary>
    /// Таймер для того, чтобы спрайт "барьера" игрока исчезал после какого-то времени без выстрелов
    /// </summary>
    private float timer = 1;

    /// <summary>
    /// Инициализация игрока; вызывается перед прорисовкой первого фрейма
    /// </summary>
    void Start() {
        _collider2d = GetComponent<Collider2D>();
        posSprite = posSword.GetComponent<SpriteRenderer>();
        posSprite.enabled = false;
    }

    /// <summary>
    /// Функция обновления с проверкой на нажатые клавиши. Вызывается каждый фрейм
    /// </summary>
    private void Update() {
        timer -= Time.deltaTime;
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckRayLength, _groundLayer);
        if (Input.GetKey(KeyCode.D)) {
            anim.SetBool("isMoving", true);
            anim.SetBool("isJumping", false);
            _movementDirection = Vector3.right;
            _creatrureAction = CreatureAction.moving;
        }
        else if (Input.GetKey(KeyCode.A)) {
            anim.SetBool("isMoving", true);
            anim.SetBool("isJumping", false);
            _movementDirection = Vector3.left;
            _creatrureAction = CreatureAction.moving;
        }
        else if (_rigidbody.velocity.x != 0.0f) {
            anim.SetBool("isMoving", false);
            _movementDirection = Vector3.zero;
            _creatrureAction = CreatureAction.stopmoving;
        }
        else {
            _movementDirection = Vector3.zero;
            _creatrureAction = CreatureAction.idle;
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) {
            anim.SetBool("isJumping", true);
            _creatrureAction = CreatureAction.jumping;  
        }
        else if (_creatrureAction == CreatureAction.jumping && _isGrounded) {
            _creatrureAction = CreatureAction.idle;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            timer = 1;
            Shooting();
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && Time.time > nextFire) {
                timer = 1;
                nextFire = Time.time + nextFire;
                Shooting();
        }
        
        if (timer <= 0 && posSprite.enabled) {
            disableBarrier();
            timer = 1;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Time.timeScale != 0) {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;  
            }
            else {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    /// <summary>
    /// Функция обновления передвижения; вызывается из таймера, независимого от частоты кадров; вызывается все, что связано с физикой
    /// </summary>
    private void FixedUpdate() {
        ModifyPhysics();
        if (_creatrureAction == CreatureAction.jumping && _isGrounded) {
            Jump();
        }
        Move();
    }

    /// <summary>
    /// Величина для определения вектора скорости
    /// </summary>
    public float scale;

    /// <summary>
    /// Функция стрельбы игрока
    /// </summary>
    void Shooting() {
        posSprite.enabled = true;
        if (scale == 1f) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(8f, GetComponent<Rigidbody2D>().velocity.y);
        }
        Instantiate(Sword, posSword.transform.position, posSword.transform.rotation);
    }

    /// <summary>
    /// Функция отключения барьера игрока после какого-то времени без выстрелов
    /// </summary>
    void disableBarrier() {
        posSprite.enabled = false;
    }
}

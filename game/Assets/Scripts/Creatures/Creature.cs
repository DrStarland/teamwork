using UnityEngine;

/// <summary>
/// Базовый класс существа
/// </summary>
public class Creature : MonoBehaviour {
    /// <summary>
    /// Перечисление возможных действий существа
    /// </summary>
    protected enum CreatureAction { 
        /// <summary>
        /// Неподвижен; начальное состояние
        /// </summary>
        idle, 

        /// <summary>
        /// Движется (вправо или влево)
        /// </summary>
        moving, 

        /// <summary>
        /// Перестал двигаться
        /// </summary>
        stopmoving, 

        /// <summary>
        /// Осуществляет прыжок
        /// </summary>
        jumping 
    };
    
    /// <summary>
    /// Текущее действие существа
    /// </summary>
    [SerializeField]
    protected CreatureAction _creatrureAction;

    /// <summary>
    /// Прямоугольный коллайдер существа; учитывается при физических столкновениях
    /// </summary>
    [SerializeField]
    protected BoxCollider2D _collider;

    /// <summary>
    /// Твердое тело существа
    /// </summary>
    protected Rigidbody2D _rigidbody;

    /// <summary>
    /// Направление движение существа
    /// </summary>
    [SerializeField]
    protected Vector2 _movementDirection;

    /// <summary>
    /// Слой, на пересечение с которым осуществляется проверка для проведения прыжка
    /// </summary>
    [SerializeField]
    protected LayerMask _groundLayer;

    /// <summary>
    /// Максимальная скорость передвижения существа
    /// </summary>
    protected const float _maxMovementSpeed = 10.0f;

    /// <summary>
    /// Минимальная скорость передвижения существа
    /// </summary>
    protected const float _minMovementSpeed = 0.0f;

    /// <summary>
    /// Сила, воздействующая на существо при движении
    /// </summary>
    protected const float _movementForce = 40.0f;

    /// <summary>
    /// Сила прыжка существа
    /// </summary>
    protected const float _jumpForce = 7.0f;

    /// <summary>
    /// Замедление существа при смене состояния движения
    /// </summary>
    protected const float _linearDrag = 30.0f;

    /// <summary>
    /// Замедление существа при прыжке
    /// </summary>
    protected const float _verticalLinearDrag = _linearDrag * 0.1f;

    /// <summary>
    /// коэффициент ускорения падения
    /// </summary>
    protected const float _fallMultiplier = 5f;

    /// <summary>
    /// Коэффициент гравитации (стандартный)
    /// </summary>
    protected const float _gravity = 1f;

    /// <summary>
    /// Удлинение луча для проверки нахождения существа на земле
    /// </summary>
    protected const float _groundCheckRayExtraLength = 0.05f;

    [SerializeField]
    /// <summary>
    /// Флаг, находится ли существо на земле
    /// </summary>
    /// 
    protected bool _isGrounded;
    
    /// <summary>
    /// Длина луча, который пускается из центра персонажа для проверки нахождения на земле
    /// </summary>
    [SerializeField]
    protected float _groundCheckRayLength;

    /// <summary>
    /// Флаг направления движения существа
    /// </summary>
    private bool facingRight;

    /// <summary>
    /// Инициализация существа; вызывается перед прорисовкой первого фрейма
    /// </summary>
    private void Awake() {
        _groundCheckRayLength = _collider.size.y / 2 * transform.localScale.y + _groundCheckRayExtraLength;
        _movementDirection = Vector2.zero;
        _creatrureAction = CreatureAction.idle;
        _isGrounded = false;
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        facingRight = true;
        
        Time.timeScale = 1;
    }

    /// <summary>
    /// Функция передвижения; направление задается пользовательским вводом. Включает в себя поворот спрайта
    /// </summary>
    protected void Move() {
        if (_rigidbody.velocity.x  > 0 && facingRight == false) {
            Flip();
        }
        else if (_rigidbody.velocity.x < 0 && facingRight == true) {
            Flip();
        }
        _rigidbody.AddForce(_movementDirection * _movementForce);

        if (Mathf.Abs(_rigidbody.velocity.x) > _maxMovementSpeed) {
            _rigidbody.velocity = new Vector2(Mathf.Sign(_rigidbody.velocity.x) * _maxMovementSpeed, _rigidbody.velocity.y);
        }
    }

    /// <summary>
    /// Функция прыжка
    /// </summary>
    protected void Jump() {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0.0f);
        _rigidbody.AddForce(Vector2.up*_jumpForce, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Обеспечение более плавного передвижения; должна вызываться после Move()
    /// </summary>
    protected void ModifyPhysics() {
        if (_isGrounded) {
            _rigidbody.gravityScale = _gravity;

            if (Mathf.Sign(_movementDirection.x) != Mathf.Sign(_rigidbody.velocity.x) || _movementDirection.x == 0.0f) {
                _rigidbody.drag = _linearDrag;
            }
            else {
                _rigidbody.drag = 0f;
            }
        }
        else {
            _rigidbody.gravityScale = _gravity;
            _rigidbody.drag = _verticalLinearDrag;

            if (_rigidbody.velocity.y < 0.0f) {
                _rigidbody.gravityScale = _gravity * _fallMultiplier;
            }
            else if (_rigidbody.velocity.y > 0.0f && _creatrureAction != CreatureAction.jumping) {
                _rigidbody.gravityScale = _gravity * _fallMultiplier / 2;
            }
            
        }
    }

    /// <summary>
    /// Поворот спрайта существа
    /// </summary>
    private void Flip() {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}

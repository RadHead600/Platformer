using UnityEngine;

public class Character : Unit
{
    [SerializeField] private Shild _shild;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private EndGame _endGameCanvas;
    [SerializeField] private RotateWeapon _rotateWeapon;
    [SerializeField] private CharacterWeapon _characterWeapon;
    [SerializeField] private CharacterAttack _characterAttack;
    [SerializeField] private CharacterStatUI _characterStatUI;

    private float _offset;
    private Coroutine _attackCoroutine;

    public float Offset
    {
        get => _offset;
        set => _offset = value;
    }
    public Rigidbody2D Rigidbody2D => _rigidbody2D;

    public const float IMPULS_UP_DEFAULT = 10.0f;
    public const float IMPULS_RIGHT_DEFAULT = 4.0f;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void FixedUpdate()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _rotateWeapon.WeaponRotate(ref _offset, difference, _characterWeapon.Hand);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_attackCoroutine == null)
                _attackCoroutine = StartCoroutine(_characterAttack.Attack());
        }
    }

    public override int ReceiveDamage(int damage)
    {
        _rigidbody2D.velocity = Vector3.zero;
        _rigidbody2D.AddForce(transform.up * IMPULS_UP_DEFAULT, ForceMode2D.Impulse);
        _rigidbody2D.AddForce(transform.right * IMPULS_RIGHT_DEFAULT, ForceMode2D.Impulse);
        HP -= damage;
        _characterStatUI.ChangeText(_characterStatUI.HpText, HP.ToString());
        if (HP <= MinHp)
            Die();

        return HP;
    }

    public override void Die()
    {
        _endGameCanvas.LoseCanvas();
    }

    public void AddShild(int addArmor)
    {
        _shild.gameObject.SetActive(true);
        _shild.AddArmor(addArmor);
    }
}

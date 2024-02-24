using System;
using UnityEngine;
using UnityEngine.UI;

public class Character : Units, IWeaponRotate
{
    public float speed;

    [SerializeField]
    private float jump;

    [SerializeField]
    private int hp;

    [SerializeField]
    private GameObject HPText;

    [SerializeField]
    private GameObject armorText;

    [SerializeField]
    private GameObject bulletsText;

    [SerializeField]
    private LayerMask block_Stay;

    [SerializeField]
    private GameObject hand;

    [SerializeField]
    private Transform body;

    [SerializeField]
    private Shild shild;

    [SerializeField]
    private GameObject[] legs;

    private float offset;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        Time.timeScale = 1;
        rigidBody = GetComponent<Rigidbody2D>();
        if(!(SaveParameters.weaponsBuy is null))
        {
            Instantiate(
                SaveParameters.weaponsBuy[SaveParameters.weaponEquip],
                new Vector3(hand.transform.position.x, hand.transform.position.y, 0),
                Quaternion.identity
                ).transform.parent = hand.transform;
        }
        else
        {
            Instantiate(
                Resources.Load<Gun>("WeaponsPrefabs/Gun"),
                new Vector3(hand.transform.position.x, hand.transform.position.y, 0),
                Quaternion.identity
                ).transform.parent = hand.transform;
        }
    }

    private void Start()
    {
        HP = hp;
    }

    private void FixedUpdate()
    {
        HPText.GetComponent<Text>().text = $"{HP}";
        armorText.GetComponent<Text>().text = $"{shild.HP}";
        bulletsText.GetComponent<Text>().text = $"{gameObject.GetComponentInChildren<Weapons>().magazine}";

        Grounded();
        Run();
    }       

    private void Update()
    {
        RotateWeapons();

        if (Input.GetButtonDown("Jump") && Grounded())
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Physics2D.IgnoreLayerCollision(10, 18, true);
            Invoke("IgnorePlatform", 0.5f);
        }

        if (Input.GetMouseButton(0))
        {
            Shoot();
        }

        if (HP <= 0)
        {
            FindObjectOfType<EndGame>().LossCanvas();
        }
    }

    private void IgnorePlatform()
    {
        Physics2D.IgnoreLayerCollision(10, 18, false);
    }

    public void RotateWeapons()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotate = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        hand.transform.rotation = Quaternion.Euler(0f, 0f, rotate + offset);

        Vector3 pos = body.transform.localScale;
        body.transform.localScale = new Vector3(
            (difference.x < 0 ? Math.Abs(pos.x) * -1 : Math.Abs(pos.x)) * (transform.localScale.x > 0 ? -1 : 1),
            pos.y,
            pos.z
            );

        if(pos.x < 0)
        {
            offset = 0;
            if (transform.localScale.x < 0)
                offset = -180;
        }
        else
        {
            offset = -180;
            if (transform.localScale.x < 0)
                offset = 0;
        }
    }

    private void Run()
    {
        float horizontal = Input.GetAxis("Horizontal") * -1;
            horizontal *= -1;

        if(horizontal != 0)
        {
            gameObject.GetComponentInChildren<Animation>().IsRun = true;
        }
        else
        {
            gameObject.GetComponentInChildren<Animation>().IsRun = false;
        }
        Vector2 movement = new Vector3(horizontal * speed * Time.fixedDeltaTime, 0.0f);
        if(horizontal >= 0)
        {
            legs[0].GetComponentInChildren<SpriteRenderer>().flipX = true;
            legs[1].GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else
        {
            legs[0].GetComponentInChildren<SpriteRenderer>().flipX = false;
            legs[1].GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        rigidBody.AddForce(movement);
    }

    public void Jump()
    {
        rigidBody.AddForce(transform.up * jump, ForceMode2D.Impulse);
    }

    public bool Grounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, .3F, block_Stay);
        return colliders.Length > 0.8;
    }

    private void Shoot()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotate = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        Weapons weaponAttack = GetComponentInChildren<Weapons>();

        if(weaponAttack.timerRecharge <= 0)
        {
            hand.transform.rotation = Quaternion.Euler(0f, 0f, rotate + offset + UnityEngine.Random.Range(-weaponAttack.spread, weaponAttack.spread));
            weaponAttack.Attack(difference);
        }
    }

    public override int ReceiveDamage(int damage)
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(transform.up * 10.0F, ForceMode2D.Impulse);
        rigidBody.AddForce(transform.right * 4.0F, ForceMode2D.Impulse);
        return HP -= damage;
    }

    public void AddShild(int addArmor)
    {
        shild.gameObject.SetActive(true);
        shild.ReceiveDamage(-addArmor);
    }
}

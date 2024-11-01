using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float hp = 10f; //ü��
    public float damage = 20f; //���ݷ�
    public float moveSpeed = 0.5f; //�̵� �ӵ�
    public int coin = 100; //��� ����
    private float maxHp;

    public float backForce = 5f;

    public bool isMov;

    public Image HpBar;

    public float hpAmount { get { return hp / maxHp; } }

    void Start()
    {
        GameManager.Instance.enemies.Add(this);
        maxHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMov)Move();
        HpBar.fillAmount = hpAmount;  
    }

    public void Move()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // ���� ���� ���� ����
        FindAnyObjectByType<background>().isScrolling = true;
        GameManager.Instance.enemies.Remove(this);
        GameManager.Instance.player.Getcoin(coin);
        GameManager.Instance.OnEnemyDefeated(this);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.TakeDamage(damage);
        }
    }

}

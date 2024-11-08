using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float hp = 10f; //체력
    public float damage = 20f; //공격력
    public float moveSpeed = 0.5f; //이동 속도
    public int coin = 100; //골드 갯수
    public ParticleSystem impactParticle;
    private float maxHp;

    public float backForce = 5f;

    public bool isMov;

    public Image HpBar;

    public float hpAmount { get { return hp / maxHp; } }

    public void SetHealth(int stage)
    {
        hp = hp + (stage * 20); // 기본 체력 , 스테이지마다 20 증가
        print($"적 생성됨: {gameObject.name}, 체력: {hp}, 스테이지: {stage}");
    }

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
        // 적이 죽을 때의 로직
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
            var paticle = Instantiate(impactParticle, other.transform.position, Quaternion.identity);
            paticle.Play();
            Destroy(paticle.gameObject, 2f);
        }
    }

    

}

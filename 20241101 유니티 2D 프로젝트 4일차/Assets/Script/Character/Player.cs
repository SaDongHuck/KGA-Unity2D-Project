using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    public background background;

    public float hp = 100f;
    public float Dmage = 5f;
    public float moveSpeed = 3f;
    public float coin = 0;
    public float maxHp;

    public Bullet missilePrefab;
    public float fireRange = 10f;

    private Transform fireDir;
    private Transform enemytarget;
    public float DamageSpeed = 5f;

    public float HpAmount { get => hp / maxHp; }
    private Coroutine fireCoroutine;

    public List<Skill> skills;
    private void Awake()
    {
        fireDir = transform.Find("FireDir");
    }

    private void Start()
    {
        StartCoroutine(FireCorutin());
        GameManager.Instance.player = this;
        maxHp = hp;
        enemytarget = GameObject.FindGameObjectWithTag("Enemy")?.transform;

        foreach (Skill skill in skills)
        {
            GameObject skillobj = Instantiate(skill.skillprefabs[skill.SkillLevel], transform, false);
            skillobj.name = skill.SkillName; //������Ʈ �̸� ����
            skillobj.transform.localPosition = Vector3.zero; //��ų ��ġ�� �÷��̾��� ��ġ�� ������

            if (skill.isTargeting)
            {
                skillobj.transform.SetParent(fireDir); //�׻�
            }
            skill.currentSkillObject = skillobj;
        }

    }

    private void Update()
    {
        /*if(enemytarget != null && Vector2.Distance(transform.position, enemytarget.position) <= fireRange)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Fire();
            }
        }*/

        if (enemytarget != null)
        {
            float distance = Vector2.Distance(transform.position, enemytarget.position);

            // ���� �߻� �Ÿ� ���� �ȿ� ���� �� �ڷ�ƾ ����
            if (distance <= fireRange)
            {
                if (fireCoroutine == null)
                {
                    //fireCoroutine = StartCoroutine(FireCorutin());
                    StartCoroutine(FireCorutin());
                }
            }
            else
            {
                // ���� ������ ����� �ڷ�ƾ ����
                if (fireCoroutine != null)
                {
                    StopCoroutine(FireCorutin());
                    fireCoroutine = null;
                }
            }
        }
    }

    public void Fire()
    {
        Bullet Bullet = Instantiate(missilePrefab, fireDir.position, Quaternion.identity);
        Bullet.transform.right = fireDir.right;
        Bullet.Damage = this.Dmage;
        Bullet.MoveSpeed = this.DamageSpeed;

    }

    public void Getcoin(int coin)
    {
        this.coin += coin; //��� ����
        print($"���� ���: {this.coin}");
    }

    private IEnumerator FireCorutin()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(2f);
            
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            //background.isScrolling = false;
            //other.GetComponent<Enemy>().isMov = false;
            background.isScrolling = false;
            var enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.isMov = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.TryGetComponent<Bullet>(out var bullet)) return;
        //background.isScrolling = true;
        background.isScrolling = true;
        var enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.isMov = true;
        }
    }

    public void heal(float amount)
    {
        hp += amount; // ä�� ȸ��
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            print("���� ����");
            //TODO : �״� �ִϸ��̼� ȣ��
            //Destroy(gameObject);
        }
    }
}

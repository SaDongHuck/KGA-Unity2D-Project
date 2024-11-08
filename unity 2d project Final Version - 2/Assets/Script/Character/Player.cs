using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Image hpBar;
    public background background;

    public float hp = 100f;
    public float Dmage = 5f;
    public float moveSpeed = 3f;
    public float coin = 0;
    public float defense = 0;
    public float maxHp;

    public float attackPowerIncrease = 10.0f;
    public float attackpower;

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

    private IEnumerator Start()
    {
        StartCoroutine(FireCorutin());
        GameManager.Instance.player = this;
        maxHp = hp;
        hpBar.fillAmount = HpAmount;
        enemytarget = GameObject.FindGameObjectWithTag("Enemy")?.transform;
        yield return null;
        foreach (Skill skill in skills)
        {
            GameObject skillobj = Instantiate(skill.skillprefabs[skill.SkillLevel], transform, false);
            skillobj.name = skill.SkillName; //������Ʈ �̸� ����
            skillobj.transform.localPosition = Vector3.zero; //��ų ��ġ�� �÷��̾��� ��ġ�� ������
            skillobj.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
            if (skill.isTargeting)
            {
                skillobj.transform.SetParent(fireDir); //�׻�
            }
            skill.currentSkillObject = skillobj;

            GameManager.Instance.playerSkill.Add(skill);
            
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
        if(hp > maxHp)
        {
            hp = maxHp; 
        }
        
    }

    public void TakeDamage(float damage)
    {
        /*hp -= damage;
        if (hp <= 0)
        {
            print("���� ����");
            //TODO : �״� �ִϸ��̼� ȣ��
            //Destroy(gameObject);
        }*/
        float actualDamage = damage - defense; // �������� ���� ���ҵ� ���ط�
        if (actualDamage < 0)
        {
            actualDamage = 0;
        }

        hp -= actualDamage;
        hpBar.fillAmount = HpAmount;

        Debug.Log($"HP: {hp}, Max HP: {maxHp}, Fill Amount: {hpBar.fillAmount}");

        if (hp <= 0)
        {
            print("���� ����");
            UIManager.Instance.GameOver();
        }

    }

   

    public void OnSkillLevelUP(Skill skill)
    {
        if (skill.SkillLevel >= skill.skillprefabs.Length - 1)
        {
            Debug.LogWarning($"�ִ� ������ ������ ��ų �������� �õ� ��{skill.SkillName}");
            return;
        }

        skill.SkillLevel++; //��ų���� ���
        GameManager.Instance.player.coin -= skill.cost;
        skill.cost += skill.costIncrease;

        print($"���� ��ų : {skill.currentSkillObject}");

        skill.currentSkillObject.SendMessage("Inactivate", SendMessageOptions.DontRequireReceiver);

        Destroy(skill.currentSkillObject); //������ �ִ� ��ų ������Ʈ�� ����
        skill.currentSkillObject = Instantiate(skill.skillprefabs[skill.SkillLevel], transform, false);
        skill.currentSkillObject.name = skill.skillprefabs[skill.SkillLevel].name;

        print($"�� ��ų : {skill.currentSkillObject.name}");
        skill.currentSkillObject.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
        skill.currentSkillObject.transform.localPosition = Vector2.zero;
        if (skill.isTargeting)
        {
            skill.currentSkillObject.transform.SetParent(fireDir);
        }

        attackpower += attackPowerIncrease * skill.SkillLevel;
        print($"���� ������:{attackpower}");

    }

    public void UseSkill(int skillID)
    {
        skills[skillID].currentSkillObject.SendMessage("Use", SendMessageOptions.DontRequireReceiver);
    }

}

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
            skillobj.name = skill.SkillName; //오브젝트 이름 변경
            skillobj.transform.localPosition = Vector3.zero; //스킬 위치를 플레이어의 위치로 가져옴
            skillobj.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
            if (skill.isTargeting)
            {
                skillobj.transform.SetParent(fireDir); //항상
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

            // 적이 발사 거리 범위 안에 있을 때 코루틴 실행
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
                // 적이 범위를 벗어나면 코루틴 중지
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
        this.coin += coin; //골드 증가
        print($"현재 골드: {this.coin}");
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
        hp += amount; // 채력 회복
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
            print("게임 종료");
            //TODO : 죽는 애니메이션 호출
            //Destroy(gameObject);
        }*/
        float actualDamage = damage - defense; // 방어력으로 인해 감소된 피해량
        if (actualDamage < 0)
        {
            actualDamage = 0;
        }

        hp -= actualDamage;
        hpBar.fillAmount = HpAmount;

        Debug.Log($"HP: {hp}, Max HP: {maxHp}, Fill Amount: {hpBar.fillAmount}");

        if (hp <= 0)
        {
            print("게임 종료");
            UIManager.Instance.GameOver();
        }

    }

   

    public void OnSkillLevelUP(Skill skill)
    {
        if (skill.SkillLevel >= skill.skillprefabs.Length - 1)
        {
            Debug.LogWarning($"최대 레벨에 도달한 스킬 레벵럽을 시도 함{skill.SkillName}");
            return;
        }

        skill.SkillLevel++; //스킬레벨 상승
        GameManager.Instance.player.coin -= skill.cost;
        skill.cost += skill.costIncrease;

        print($"현재 스킬 : {skill.currentSkillObject}");

        skill.currentSkillObject.SendMessage("Inactivate", SendMessageOptions.DontRequireReceiver);

        Destroy(skill.currentSkillObject); //기존에 있던 스킬 오브젝트를 제거
        skill.currentSkillObject = Instantiate(skill.skillprefabs[skill.SkillLevel], transform, false);
        skill.currentSkillObject.name = skill.skillprefabs[skill.SkillLevel].name;

        print($"새 스킬 : {skill.currentSkillObject.name}");
        skill.currentSkillObject.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
        skill.currentSkillObject.transform.localPosition = Vector2.zero;
        if (skill.isTargeting)
        {
            skill.currentSkillObject.transform.SetParent(fireDir);
        }

        attackpower += attackPowerIncrease * skill.SkillLevel;
        print($"현재 전투력:{attackpower}");

    }

    public void UseSkill(int skillID)
    {
        skills[skillID].currentSkillObject.SendMessage("Use", SendMessageOptions.DontRequireReceiver);
    }

}

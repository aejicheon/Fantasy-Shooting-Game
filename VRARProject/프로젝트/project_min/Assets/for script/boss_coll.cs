using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class boss_coll : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public AudioSource audiosource;
   
    public AudioSource hit;
    public GameObject target;
    public Animator anim;
    public GameObject darkball;
    float darkball_time = 0.0f;
    bool dark_flag = true;
    public GameObject[] darkball_spawn = new GameObject[5];
    public GameObject[] sword_spawn = new GameObject[6];
    public GameObject[] sword = new GameObject[6];
    bool dark_ballf = true;
    bool swordt = true;
    bool sword_flag = true;
    float sword_time = 0.0f;
    bool move_flag = true;
    bool time_flag = true;
    float second_attack = 0.0f;
    float first_attack = 0.0f;
    bool first_flag = true;
    public GameObject hp;
    float time = 0.0f;
    bool time_fl = true;
    bool death_flag = false;
    float d_time = 0;
    public GameObject boss_hp;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject tmp = Instantiate(boss_hp) as GameObject;
        target = GameObject.Find("unitychan_dynamic");
        hp = tmp;
        GameObject t = GameObject.Find("Canvas");
        tmp.transform.parent = t.gameObject.transform;
       // tmp.rc.anchoredPosition = new Vector2(686f,390.4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (death_flag)
        {
            time = 0;
            anim.SetTrigger("die");
            move_flag = false;
            d_time += Time.deltaTime;
        }
        if(d_time > 5)
        {
            SceneManager.LoadScene("Winscene");
        }
            time += Time.deltaTime;
        if (time_fl&&time > 3.0f)
        {
            Debug.Log("Zzz");
            agent.SetDestination(target.transform.position);
            anim.SetBool("boss_move", true);
            time_fl = false;
        }
        else if (time > 3.0f)
        {
            if (!first_flag)
            {
                first_attack += Time.deltaTime;
                move_flag = false;
                if (first_attack > 3.0f)
                {
                    move_flag = true;
                    first_flag = true;
                    anim.SetBool("boss_move", true);
                    first_attack = 0.0f;
                }
            }
            if (!time_flag)
            {
                second_attack += Time.deltaTime;
                move_flag = false;
                if (second_attack > 3.0f)
                {
                    time_flag = true;
                    move_flag = true;
                    anim.SetBool("boss_move", true);
                    second_attack = 0.0f;
                }
            }
            if (!dark_flag)
            {
                darkball_time += Time.deltaTime;
                if (darkball_time > 1.5f && dark_ballf)
                {
                    move_flag = true;
                    dark_ballf = false;
                }
                if (darkball_time > 10.0f)
                {
                    darkball_time = 0.0f;
                    dark_flag = true;
                }
            }
            if (!sword_flag)
            {
                sword_time += Time.deltaTime;
                if (sword_time > 1.5f && swordt)
                {
                    move_flag = true;
                    swordt = false;
                }
                if (sword_time > 13.0f)
                {
                    sword_time = 0.0f;
                    sword_flag = true;
                }
            }
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            if (move_flag) agent.SetDestination(target.transform.position);
            else agent.SetDestination(this.transform.position);
            if (Physics.Raycast(this.transform.position, fwd, out hit, 15) == true && hit.collider.gameObject.tag == "Player" && dark_flag && move_flag)
            {
                move_flag = false;
                anim.SetBool("boss_move", false);
                anim.SetTrigger("boss_attack");
                agent.SetDestination(this.transform.position);
                dark_flag = false;
                darkball_time = 0.0f;
                dark_ballf = true;
                for (int i = 0; i < 5; i++)
                {
                    Instantiate(darkball, darkball_spawn[i].transform.position, darkball_spawn[i].transform.rotation);
                }
            }
            if (Physics.Raycast(this.transform.position, fwd, out hit, 25) == true && hit.collider.gameObject.tag == "Player" && sword_flag && move_flag)
            {
                move_flag = false;
                anim.SetBool("boss_move", false);
                anim.SetTrigger("boss_attack");
                agent.SetDestination(this.transform.position);
                sword_flag = false;
                sword_time = 0.0f;
                swordt = true;
                for (int i = 0; i < 8; i++)
                {
                    Instantiate(sword[i], sword_spawn[i].transform.position, sword_spawn[i].transform.rotation);
                }
            }
        }
    }
    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Attack")
        {
            if (move_flag)
            {
                anim.SetBool("boss_move", false);
                anim.SetTrigger("boss_damage");
            }
            move_flag = false;
            first_flag = false;
            agent.SetDestination(this.transform.position);
            hp.GetComponent<HP_script>().HealthBar.value -= 0.1f;
            if(hp.GetComponent<HP_script>().HealthBar.value == 0.0f)
            {
                anim.Rebind();
                death_flag = true;
                audiosource.Play();
            }
            else hit.Play();
        }
        if(coll.gameObject.tag == "hidden_Attack")
        {
            if (move_flag)
            {
                anim.SetBool("boss_move", false);
                anim.SetTrigger("boss_damage");
            }
            move_flag = false;
            first_flag = false;
            agent.SetDestination(this.transform.position);
            hp.GetComponent<HP_script>().HealthBar.value -= 0.4f;
            if (hp.GetComponent<HP_script>().HealthBar.value == 0.0f)
            {
                anim.Rebind();
                death_flag = true;
                audiosource.Play();
            }
            else hit.Play();
        }
        
    }
    void OnCollisionStay(Collision coll)
    {
        if(coll.gameObject.tag == "TimeAttack")
        {
            if (move_flag) anim.Rebind();
            time_flag = false;
            second_attack = 0.0f;
            agent.SetDestination(this.transform.position);
        }
    }
  
}

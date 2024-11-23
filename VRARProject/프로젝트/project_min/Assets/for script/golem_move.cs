using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golem_move : MonoBehaviour
{
    public AudioSource audioSource;
    public float time = 0.0f;
    public UnityEngine.AI.NavMeshAgent agent;
    public Animator anim;
    GameObject target;
    bool move_flag = true;
    float throw_time = 10.0f;
    float fix_throw_time = 10.0f;
    float hit_throw_time = 10.0f;
    float hit_time = 5.0f;
    bool throw_flag = false;
    public GameObject rock_spawn;
    public GameObject rock;
    public int life = 3;
    public float death_time = 0.0f;
    public float gethit_time = 0.0f;
    public bool gethit_flag = false;
    public bool time_flag = false;
    public float timea_time = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("unitychan_dynamic");
        anim.SetBool("walk_flag", false);
    }

    // Update is called once per frame
    void Update()
    {
        throw_time += Time.deltaTime;
        hit_time += Time.deltaTime;
        time += Time.deltaTime;
        if (life <= 0)
        {
            death_time += Time.deltaTime;
            if (death_time > 3)
            {
                target.GetComponent<player_control>().enemycount++;
                Destroy(this.gameObject);
                return;

            }
        }
        else if (gethit_flag)
        {
            gethit_time += Time.deltaTime;
        }
        else if (time_flag)
        {
            timea_time += Time.deltaTime;
        }
        else if (throw_time < 3.0f || hit_time < 2.5)
        {
            if (throw_time > 0.6f && throw_flag)
            {
                GameObject tmp = Instantiate(rock, rock_spawn.transform.position, rock_spawn.transform.rotation) as GameObject;
                Vector3 f = tmp.transform.TransformDirection(Vector3.forward);
                tmp.GetComponent<Rigidbody>().AddForce(f * 1000);
                throw_flag = false;
            }
        }
        else if (time < 2.0f)
        {

        }
        else
        {
            agent.SetDestination(target.transform.position);
            if (move_flag && time > 3.0f)
            {
                anim.SetBool("walk_flag", true);
                move_flag = false;
            }

            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(this.transform.position, fwd, out hit, 5) == true && hit.collider.gameObject.tag == "Player" && hit_time > hit_throw_time)
            {
                hit_time = 0.0f;
                anim.SetBool("walk_flag", false);
                anim.SetTrigger("punch_rock");
                agent.SetDestination(this.transform.position);
                move_flag = true;
            }
            else if (Physics.Raycast(this.transform.position, fwd, out hit, 10) == true && hit.collider.gameObject.tag == "Player" && throw_time > fix_throw_time)
            {
                throw_time = 0.0f;
                anim.SetBool("walk_flag", false);
                anim.SetTrigger("throw_rock");
                agent.SetDestination(this.transform.position);
                move_flag = true;
                throw_flag = true;
            }
        }
        if (gethit_time > 2.0f)
        {
            gethit_flag = false;
            gethit_time = 0.0f;
            move_flag = true;
        }
        if (timea_time > 8.0f)
        {
            time_flag = false;
            timea_time = 0.0f;
        }
    }
    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "hidden_Attack")
        {
            gethit_flag = true;
            gethit_time = 0.0f;
            anim.Rebind();
            life = 0;
            anim.SetTrigger("s_death");
            agent.SetDestination(this.transform.position);
            audioSource.Play();
        }
        else if (coll.gameObject.tag == "Attack" && life>0)
        {
            if (throw_time < 2.5f)
            {
                return;
            }
            gethit_flag = true;
            gethit_time = 0.0f;
            anim.Rebind();
            life--;
            audioSource.Play();
            if (life > 0)
            {
                anim.SetTrigger("get_hit");
                agent.SetDestination(this.transform.position);
            }
            else
            {
                anim.SetTrigger("s_death");
                agent.SetDestination(this.transform.position);
            }
        }
    }
    void OnCollisionStay(Collision coll)
    {

        if(coll.gameObject.tag == "TimeAttack" && life > 0)
        {
            if (throw_time < 2.5f)
            {
                return;
            }
            anim.Rebind();
            time_flag = true;
            agent.SetDestination(this.transform.position);
            move_flag = true;
        }
    }
}

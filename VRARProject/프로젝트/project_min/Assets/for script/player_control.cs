using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityChan;
using UnityEngine.SceneManagement;
public class player_control : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioclip;
    [SerializeField] private AudioClip[] clip;

    public GameObject aeji_ball;
    public GameObject jiae_ball;
    public GameObject aeji_hill;
    public GameObject fireball_spawn;
    public GameObject hillzone_spawn;
    public GameObject jiaeball_spawn;
    public GameObject hp;
    public Animator anim;
    public bool move_flag = true;
    float time = 0.0f;
    bool aeji_flag = false;
    bool jiae_flag = false;
    bool hiller_flag = false;
    bool coll_flag = true;
    float coll_time = 0.0f;
    public float aeji_ballt = 0.0f;
    public float jiae_ballt = 0.0f;
    public float aeji_hillt = 0.0f;
    float aeji_balltime = 4.0f;
    float jiae_balltime = 12.0f;
    float aeji_hilltime = 20.0f;
    bool aeji_ballflag = true;
    bool jiae_ballflag = true;
    bool aeji_hillflag = true;
    public GameObject sword;
    public GameObject sword_spawn;
    bool sword_flag = false;
    float sword_time = 0.0f;
    bool aeji_swordflag = true;
    public float aeji_swordt = 0.0f;
    public int enemycount = 0;
    public GameObject Boss;
    public GameObject Boss_spawn;
    bool boss_flag = true;
    public GameObject Boss_particle;
    bool death_flag = false;
    float death_time = 0.0f;
    public AudioSource hit_a;
    // Start is called before the first frame update
    void Start()
    {
        // audioSource = GetComponent<AudioSource>();
        Debug.Log(time);
    }
    public void Play()
    {
        //int r = 1;
        //audioSource.clip = clip[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (death_flag)
        {
            move_flag = false;
            death_time += Time.deltaTime;
        }
        if(death_time > 5)
        {
            SceneManager.LoadScene("loseScene");
        }
        if (!coll_flag) coll_time += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.Rotate(0, 180, 0);
        }
        if (!aeji_ballflag)
        {
            aeji_ballt -= Time.deltaTime;
            if(aeji_ballt - Time.deltaTime < 0)
            {
                aeji_ballt = 0;
                aeji_ballflag = true;
            }
        }
        if (!jiae_ballflag)
        {
            jiae_ballt -= Time.deltaTime;
            if (jiae_ballt- Time.deltaTime < 0)
            {
                jiae_ballt = 0;
                jiae_ballflag = true;
            }
        }
        if (!aeji_hillflag)
        {
            aeji_hillt -= Time.deltaTime;
            if (aeji_hillt - Time.deltaTime < 0)
            {
                aeji_hillt = 0;
                aeji_hillflag = true;
            }
        }
        if (!aeji_swordflag)
        {
            aeji_swordt -= Time.deltaTime;
            if (aeji_swordt - Time.deltaTime < 0) {
                aeji_swordt = 0;
                aeji_swordflag = true;
            }
        }
        if (!move_flag) time += Time.deltaTime;
        if(time > 2.4f&& aeji_flag)
        {
            time = 0.0f;
            move_flag = true;
            aeji_flag = false;
        }
        if (time > 2.2f && jiae_flag)
        {
            time = 0.0f;
            move_flag = true;
            jiae_flag = false;
        }
        if (time > 1.7f && hiller_flag)
        {
            time = 0.0f;
            move_flag = true;
            hiller_flag = false;
        }
        if(coll_time > 1.0f && !aeji_flag && !jiae_flag && !hiller_flag && !sword_flag)
        {
            move_flag = true;
          
        }
        if(coll_time > 3.0f)
        {
            coll_time = 0.0f;
            coll_flag = true;
            
        }
        if(time > 2.2f && sword_flag)
        {
            time = 0.0f;
            move_flag = true;
            sword_flag = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && move_flag && aeji_ballflag)
        {
            Instantiate(aeji_ball, fireball_spawn.transform.position, this.transform.rotation);
            anim.SetTrigger("fireball_shoot");
            move_flag = false;
            aeji_flag = true;
            aeji_ballflag = false;
            aeji_ballt = aeji_balltime; 
        }
        if(Input.GetKeyDown(KeyCode.R) && move_flag && aeji_swordflag)
        {
            Instantiate(sword,sword_spawn.transform.position, sword_spawn.transform.rotation);
            anim.SetTrigger("fireball_shoot");
            move_flag = false;
            sword_flag = true;
            aeji_swordflag = false;
            aeji_swordt = 40.0f;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && move_flag && jiae_ballflag)
        {
            GameObject tmp = Instantiate(jiae_ball, jiaeball_spawn.transform.position, this.transform.rotation) as GameObject;
            tmp.transform.parent = tmp.transform;
            anim.SetTrigger("jiae_ball");
            move_flag = false;
            jiae_flag = true;
            jiae_ballflag = false;
            jiae_ballt = jiae_balltime;
        }
        if (Input.GetKeyDown(KeyCode.E) && move_flag && aeji_hillflag)
        {
            Instantiate(aeji_hill, hillzone_spawn.transform.position, aeji_hill.transform.rotation);
            anim.SetTrigger("aeji_hill");
            hp.GetComponent<HP_script>().HealthBar.value += 0.3f;
            move_flag = false;
            hiller_flag = true;
            aeji_hillflag = false;
            aeji_hillt = aeji_hilltime;
        }
        if(enemycount > 9 && boss_flag)
        {
            boss_flag = false;
            Instantiate(Boss, Boss_spawn.transform.position, Boss_spawn.transform.rotation);
            Instantiate(Boss_particle, Boss_spawn.transform.position, Boss_spawn.transform.rotation);
        } 
    }
    void OnCollisionStay(Collision coll)
    {
        if(coll.gameObject.tag == "Enemy") hp.GetComponent<HP_script>().HealthBar.value -= 0.004f;
        if (hp.GetComponent<HP_script>().HealthBar.value == 0.0f && !death_flag)
        {
            death_flag = true;
            anim.Rebind();
            anim.SetTrigger("lose");
            audioSource.Play();
        }
    }
    void OnCollisionEnter(Collision coll) {
        if (coll_flag && coll.gameObject.tag == "rockt")
        {
            if (move_flag)
            {
                anim.SetTrigger("gethit");
                coll_flag = false;
                move_flag = false;
            }

            
            hp.GetComponent<HP_script>().HealthBar.value -= 0.1f;
            if (hp.GetComponent<HP_script>().HealthBar.value == 0.0f && !death_flag)
            {
                death_flag = true;
                anim.Rebind();
                anim.SetTrigger("lose");
                audioSource.Play();
            }
            else hit_a.Play();
        }
        if(coll_flag && coll.gameObject.tag == "boss_attack")
        {
            if (move_flag)
            {
                anim.SetTrigger("gethit");
                coll_flag = false;
                move_flag = false;
               
            }
            
            hp.GetComponent<HP_script>().HealthBar.value -= 0.2f;
            if (hp.GetComponent<HP_script>().HealthBar.value == 0.0f && !death_flag)
            {
                death_flag = true;
                anim.Rebind();
                anim.SetTrigger("lose");
                audioSource.Play();
            }
            else hit_a.Play();
        }
        
    }
    GUIStyle style = new GUIStyle();

    private void Awake()
    {
        style.fontSize = 25;
        style.normal.textColor = Color.red;
    }
    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2-80, 5, 10, 10), "Kill : ", style);
        GUI.Label(new Rect(Screen.width / 2, 5, 10, 10), enemycount.ToString(), style);
    }
}

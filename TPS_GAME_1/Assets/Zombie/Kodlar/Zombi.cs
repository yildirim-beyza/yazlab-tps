using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Zombi : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float zombiHP = 100;
    bool zombiOlu;
    Animator zombiAnim;
    GameObject hedefOyuncu;
    public float kovalamaMesafesi;
    NavMeshAgent zombiNavmesh;
    public float saldirmaMesafesi;
    float mesafe;


    void Start()
    {
        zombiAnim = this.GetComponent<Animator>();
        hedefOyuncu = GameObject.Find("SWAT");
        zombiNavmesh=this.GetComponent <NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (zombiHP <= 0)
        {
            zombiOlu = true;

        }
        if (zombiOlu == true)
        {
            zombiAnim.SetBool("oldu", true);
            StartCoroutine(YokOl());
        }
        else
        {
            mesafe = Vector3.Distance(this.transform.position, hedefOyuncu.transform.position);
            if (mesafe < kovalamaMesafesi)
            {
                //koþma
                zombiNavmesh.isStopped = false;
                zombiNavmesh.SetDestination(hedefOyuncu.transform.position);
                zombiAnim.SetBool("yuruyor", true);
                zombiAnim.SetBool("saldýrýyor", true);
                this.transform.LookAt(hedefOyuncu.transform.position);
            }
            else
            {
                //durma animasyon
                zombiNavmesh.isStopped = true;
                zombiAnim.SetBool("yuruyor", false);
                zombiAnim.SetBool("saldýrýyor", false);
            }
            if (mesafe < saldirmaMesafesi)
            {
                this.transform.LookAt(hedefOyuncu.transform.position);
                zombiNavmesh.isStopped = true;
                //vurma animasyon
                zombiAnim.SetBool("yuruyor", false);
                zombiAnim.SetBool("saldýrýyor", false);
            }
        }

    }
    public void HasarVer()
    {
       // hedefOyuncu.GetComponent<KarakterKontrol>
    }
    IEnumerator YokOl()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
    public void HasarAl()
    {
        zombiHP -= Random.Range(15, 25);
    }
}

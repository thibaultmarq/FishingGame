using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Note : MonoBehaviour
{
    [SerializeField]
    GameObject noteLine;

    [SerializeField]
    private float speed;
    public float Speed { get { return speed; } set {    speed = value; } }

    [SerializeField]
    private int noteType;

    public int NoteType {  get { return noteType; } set { noteType = value; } }

    [SerializeField]
    private float lifespan;
    public float Lifespan { get {   return lifespan; } set {    lifespan = value; } }

    [SerializeField]
    private float lifetime;
    public float Lifetime {  get { return lifetime; } set {  lifetime = value; } } 


    public void Disposal()
    {
        GameManager.Instance.noteQueue.RemoveAt(0);
        Destroy(this.gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        Lifespan = 2*transform.position.x / speed;
    }

    // Update is called once per frame
    void Update()
    {

        if (lifetime >= lifespan)
        {

            FishHealthBar.Instance.HealthUpdate(-2);
            Disposal();
            FishHealthBar.Instance.IncrementErrorCounter();
        }
        else
        {
            Vector3 new_pos = transform.position;
            new_pos.x -= Time.deltaTime * speed;
            transform.position = new_pos;
            lifetime += Time.deltaTime;
        }
        
    }
}

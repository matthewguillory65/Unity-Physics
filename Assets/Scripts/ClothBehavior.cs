using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
 #if UNITY_EDITOR
    using UnityEditor;
#endif
public class ClothBehavior : MonoBehaviour, IDragHandler
{
    //public Collider coll;
    public List<GameObject> Spheres = new List<GameObject>();
    public List<HookesLaw.Particle> particles;
    public List<HookesLaw.SpringDamper> dampener;
    public GameObject Object;
    int rows = 4;
    int cols = 4;
    float spacing = 1;
    public float restPosition;
    public int constant;
    public int dampening;
    public GameObject part;
    HookesLaw.Particle part2;

    List<GameObject> Objects;



    // Use this for initialization
    void Start()
    {
        Objects = new List<GameObject>();
        dampener = new List<HookesLaw.SpringDamper>();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                var y = -i * rows * restPosition;
                var z = -j * cols * restPosition;
                part = Instantiate(Object);
                part.transform.position = new Vector3(0, y, z);
                Objects.Add(part);
                part2 = new HookesLaw.Particle(part.transform.position);
                particles.Add(part2);
                
                
            }
        }
        Spheres.Sort();


        //coll = GetComponent<Collider>();
        //coll.isTrigger = true;


        for(int i = 0; i < rows * cols; i++)
        {
            //Horizontal Connections
            if (i / cols != cols - 1)
            {
                dampener.Add(new HookesLaw.SpringDamper(particles[i * rows], particles[(rows - 1) + (i * rows)], 10f, 5));
            }

            //Vertical Connections
            if (i < (cols * rows) - cols)
            {
                dampener.Add(new HookesLaw.SpringDamper(particles[(cols - 1) + (i * cols)], particles[i * cols], 10f, 5));
            }

            //Left-right Diag connections


            //Right-left Diag connections

            ///Horizontal Bending
            ///Vertical Bending
        }
        //for (int i = 0; i < rows; i++)
        //    for (int j = 0; j < cols; j++)
        //    {
        //        dampener.Add(new HookesLaw.SpringDamper(particles[i + (j * cols)], particles[j + 1 + (i * rows)], 10f, 5));
        //        dampener.Add(new HookesLaw.SpringDamper(particles[i * rows], particles[ 1 + (j * cols)], 10f, 5));
        //    }
    }

    // Update is called once per frame
    void Update()
    {        
        foreach (var j in particles)
        {
            //j.AddForce(new Vector3(0, -9.8f, 0));
            //j.Update(Time.deltaTime);
        }
        for(int w = 0; w < 16; w++)
        {
            Objects[w].transform.position = particles[w].position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.attachedRigidbody)
    //    {
    //        other.attachedRigidbody.useGravity = false;
    //    }
    //}

#if UNITY_EDITOR
    void OnDrawGizmos()
    {        
        if (dampener != null)
        foreach(var damp in dampener)
        {
            Gizmos.DrawLine(damp._p1.position, damp._p2.position);
        }
    }
#endif
}

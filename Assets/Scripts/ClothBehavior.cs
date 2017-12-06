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
    public List<GameObject> Spheres = new List<GameObject>();
    public List<HookesLaw.Particle> particles;
    public List<HookesLaw.SpringDamper> dampener;
    public GameObject Object;
    int rows = 4;
    int cols = 4;
    float spacing = 1;
    public float restPosition = 1.5f;
    public int constant = 200;
    public int dampening = 100;
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

        //foreach(var i in particles)
        //{
        //    i.AddForce(new Vector3(-40, 0, 0));
        //}



        for(int i = 0; i < rows * cols; i++)
        {
            bool greaterZero = i > 0;
            bool lessThanRightSideColumn = i % cols < cols - 1;
            bool leftSideColumn = i % cols == 0;
            bool lessthanbottomRow = i < (cols * rows) - cols;
            //Horizontal Connections
            if (lessThanRightSideColumn)
            {
                dampener.Add(new HookesLaw.SpringDamper(particles[i], particles[i + 1], constant, restPosition));
            }

            ////Vertical Connections
            if (lessthanbottomRow)
            {
                dampener.Add(new HookesLaw.SpringDamper(particles[i], particles[i + (rows)], constant, restPosition));
            }

            //Left - right Diag connections
            if (lessthanbottomRow && lessThanRightSideColumn)
            {
                int bottom = i + cols;
                int right = i + 1;
                dampener.Add(new HookesLaw.SpringDamper(particles[right], particles[bottom], constant, restPosition));
            }

            //Right-left Diag connections
            if (lessthanbottomRow && lessThanRightSideColumn /*&& !rightSideColumn*/)
            {
                int bottom = i + cols;
                int bottomRight = bottom + 1;
                dampener.Add(new HookesLaw.SpringDamper(particles[i], particles[bottomRight], constant, restPosition));

            }
            

            



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
            
            if (j != particles[0] && j != particles[cols - 1])
                j.AddForce(new Vector3(0, -9.8f, 0));
            j.Update(Time.deltaTime);
        }
        for(int w = 0; w < 16; w++)
        {
            Objects[w].transform.position = particles[w].position;

            if (w == 0)
            {
                particles[0].AddForce(new Vector3(0, 0, 0));
            }

            if (w == cols - 1)
            {
                particles[cols - 1].AddForce(new Vector3(0, 0, 0));
            }
        }
        foreach (var i in dampener)
        {
            i.BacktoNormal();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody)
        {
            other.attachedRigidbody.useGravity = false;
        }
    }

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

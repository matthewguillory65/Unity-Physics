using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
 #if UNITY_EDITOR
    using UnityEditor;
#endif
public class ClothBehavior : MonoBehaviour
{
    public List<GameObject> Spheres = new List<GameObject>();
    public List<HookesLaw.Particle> particles;
    public List<HookesLaw.SpringDamper> dampener;
    public GameObject Object;
    int rows = 4;
    int cols = 4;
    float spacing = 1;
    public float restPosition = .65f;
    public float constant = 10;
    public float dampening = 3;
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
                var y = -i * restPosition;
                var z = -j * restPosition;
                part = Instantiate(Object);
                part.transform.position = new Vector3(0, y, z);
                Objects.Add(part);
                part2 = new HookesLaw.Particle(part.transform.position);
                particles.Add(part2);
                
                
            }
        }
        Spheres.Sort();
        
        for (int i = 0; i < rows * cols; i++)
        {
            bool greaterZero = i > 0;
            bool lessThanRightSideColumn = i % cols < cols - 1;
            bool leftSideColumn = i % cols == 0;
            bool lessthanbottomRow = i < (cols * rows) - cols;
            //Horizontal Connections
            if (lessThanRightSideColumn)
            {
                dampener.Add(new HookesLaw.SpringDamper(particles[i], particles[i + 1], constant, dampening, restPosition));
            }
            ////Vertical Connections
            if (lessthanbottomRow)
            {
                dampener.Add(new HookesLaw.SpringDamper(particles[i], particles[i + (rows)], constant, dampening, restPosition));
            }
            //Left - right Diag connections
            if (lessthanbottomRow && lessThanRightSideColumn)
            {
                int bottom = i + cols;
                int right = i + 1;
                dampener.Add(new HookesLaw.SpringDamper(particles[right], particles[bottom], constant, dampening, restPosition * 1.41f));
            }
            //Right-left Diag connections
            if (lessthanbottomRow && lessThanRightSideColumn)
            {
                int bottom = i + cols;
                int bottomRight = bottom + 1;
                dampener.Add(new HookesLaw.SpringDamper(particles[i], particles[bottomRight], constant, dampening, restPosition * 1.41f));

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var j in particles)
        {
            j.Update(Time.deltaTime);
        }
        for(int w = 0; w < 16; w++)
        {
            Objects[w].transform.position = particles[w].position;

            if (w == 0)
            {
                particles[0].useGravity = false;
                particles[0].locked = true;
                particles[0].velocity = Vector3.zero;
            }
            if (w == cols - 1)
            {
                particles[w].useGravity = false;
                particles[w].locked = true;
                particles[w].velocity = Vector3.zero;
            }
        }
        foreach (var i in dampener)
        {
            i.BacktoNormal();
        }
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

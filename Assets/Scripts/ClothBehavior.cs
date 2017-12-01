using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothBehavior : MonoBehaviour
{

    public List<GameObject> Spheres = new List<GameObject>();
    public List<HookesLaw.Particle> particles;
    public List<HookesLaw.SpringDamper> dampener;
    public GameObject Object;
    public Vector3 position;
    int rows = 3;
    int cols = 3;
    int spacing = 1;
    public int restPosition;
    public int constant;
    public int dampening;

    List<GameObject> Objects;



    // Use this for initialization
    void Start()
    {
        Objects = new List<GameObject>();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                var y = -i * rows * restPosition;
                var z = -j * cols * restPosition;
                GameObject part = Instantiate(Object);
                part.transform.position = new Vector3(0, y, z);
                Objects.Add(part);
                HookesLaw.Particle part2 = new HookesLaw.Particle(part.transform.position);
                particles.Add(part2);
            }
        }
        Spheres.Sort();



        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
            {
                dampener.Add(new HookesLaw.SpringDamper(particles[i], particles[i + 1], spacing, 10));

            }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var i in dampener)
        {

        }
    }
}

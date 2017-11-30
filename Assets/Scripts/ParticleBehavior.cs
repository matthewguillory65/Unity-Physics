using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehavior : MonoBehaviour {
    [SerializeField]
    public GameObject firstSphere;
    public GameObject secondSphere;

    public HookesLaw.Particle particle;
    public HookesLaw.Particle part1;
    public HookesLaw.Particle part2;
    public HookesLaw.SpringDamper dampener;

	// Use this for initialization
	void Start ()
    {
        part1 = new HookesLaw.Particle(firstSphere.transform.position);
        part2 = new HookesLaw.Particle(secondSphere.transform.position);
        dampener = new HookesLaw.SpringDamper(part1, part2, 1f, 5);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.position = particle.Update(Time.fixedDeltaTime);
        part1.transform.position = part1.position;
        part2.transform.position = part2.position;
	}
}

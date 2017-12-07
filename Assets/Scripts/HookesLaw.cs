using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HookesLaw
{
    [System.Serializable]
    public class Particle
    {
        public Particle(Vector3 pos)
        {
            useGravity = true;
            locked = false;
            force = Vector3.zero;
            mass = 1;
            acceleration = Vector3.zero;
            velocity = Vector3.zero;
            position = pos;
        }

        public Vector3 position;
        public Vector3 velocity;
        Vector3 acceleration;
        //not weight
        float mass;//must be at least 1
        Vector3 force;
        public bool useGravity;
        public bool locked;

        public void AddForce(Vector3 f)
        {
            force += f;
        }

        public void Update(float deltaTime)
        {
            if(useGravity)
            {
                force += new Vector3(0, -9.81f, 0);
            }
            if(!locked)
            {
                acceleration = force / mass;
                velocity += acceleration * deltaTime;
                position += velocity * deltaTime;
                force = Vector3.zero;
            }
        }
    }

    public class SpringDamper
    {
        public Particle _p1, _p2, particles;
        float _Ks;//Spring Constant
        float _Lo;//Rest Length
        float _Kd;//Spring Damp

        public SpringDamper()
        { }

        public SpringDamper(Particle p1, Particle p2, float springConstant, float springDamp, float restLength)
        {
            _p1 = p1;
            _p2 = p2;
            _Ks = springConstant;
            _Lo = restLength;
            _Kd = springConstant;
        }

        public void BacktoNormal()
        {
            Vector3 estar = _p2.position - _p1.position;
            float _l = estar.magnitude;
            Vector3 _E = estar / _l;

            float _v1 = Vector3.Dot(_E, _p1.velocity);
            float _v2 = Vector3.Dot(_E, _p2.velocity);

            float Fsd = (-_Ks * (_Lo - _l)) - (_Kd * (_v1 - _v2));

            Vector3 _f1 = Fsd * _E;
            Vector3 _f2 = -_f1;
            
            _p1.AddForce(_f1);
            _p2.AddForce(_f2);

            
        }
    }

    public class Wind
    {

    }


}

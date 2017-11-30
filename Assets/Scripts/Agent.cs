using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace matt
{
    abstract class Agent : MonoBehaviour
    {
        protected float mass;
        protected Vector3 velocity;
        protected Vector3 acceleration;
        protected Vector3 position;
        protected float max_speed;

        abstract public void Update_Agent();

        abstract public bool Add_Force(float m, Vector3 force);

        abstract public void Update();

        abstract public void Late_Void_Agent();
    }

}


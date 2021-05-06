using System;
using System.Collections.Generic;
using UnityEngine;

namespace Steering
{// composite means takes other behaviours and combines them (adds both force together.)

    [CreateAssetMenu(menuName = "Steering/Composite", fileName = "Composite" , order = -100)]
    public class CompositeBehaviour : SteeringBehaviour
    {// they go on a waiting system, things with high wait(weight?) = more influence

        [Serializable]
        public struct WeightedBehavior
        {
            // can't set default values in structs
            public float weighting;
            public SteeringBehaviour behaviour;
        }

        [SerializeField] private List<WeightedBehavior> behaviours = new List<WeightedBehavior>();
        public override Vector3 Calculate(SteeringAgent _agent)
        {
            Vector3 force = _agent.CurrentForce;
            // need to loop through behaviour,
            // calc each force,
            // add together, 
            // multiply by weight

            // inline forEach loop
            behaviours.ForEach(weighted =>
            {
                force += weighted.behaviour.Calculate(_agent) * weighted.weighting;
            });

            return force;
        }
    }
}
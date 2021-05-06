using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering
{
    [CreateAssetMenu(menuName = "Steering/Avoidance", fileName = "Avoidance")]
    public class AvoidanceBehaviour : SteeringBehaviour
    {
        [SerializeField] private float viewDistance = 1f;
        [SerializeField, Range(.1f, .9f)] private float normalRatio = .35f;

        public override Vector3 Calculate(SteeringAgent _agent)
        {
            Vector3 force = _agent.CurrentForce;
            
            foreach (Vector3 direction in SteeringAgentHelper.DirectionsInCone(_agent))
            {
                if(Physics.Raycast(_agent.Position, direction, out RaycastHit hit, viewDistance))
                {
                    // * visualise the collision
                    Debug.DrawLine(_agent.Position, hit.point, Color.red);

                    // * Interpolate the normal by the forward over the normalRatio variable
                    force += Vector3.Lerp(_agent.Forward, hit.normal, normalRatio);
                }
            }


            return force;
            
        }

        
    }
}
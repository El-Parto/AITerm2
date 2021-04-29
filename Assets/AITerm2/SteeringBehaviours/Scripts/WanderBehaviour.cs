using UnityEngine;


namespace Steering
{

    [CreateAssetMenu(menuName = "Steering/Wander", fileName = "wander")]
    public class WanderBehaviour : SteeringBehaviour
    {
        [SerializeField, Range(.01f, .1f)] private float jitter = .05f;

        protected override Vector3 Calculate(SteeringAgent _agent)
        {
            //getting the force

            Vector3 force = _agent.CurrentForce;

            Vector2 offset = new Vector2(Random.Range(-jitter, jitter), Random.Range(-jitter, jitter));

            force += _agent.Right * offset.x;
            force += _agent.Up * offset.y;

            return force;
            // May the fourth be with you.

        }
    }
}
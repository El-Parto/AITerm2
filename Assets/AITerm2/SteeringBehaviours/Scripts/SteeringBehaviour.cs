using UnityEngine;


namespace Steering
{
    public abstract class SteeringBehaviour : ScriptableObject
    {

        /// <summary>
        /// * Runs the calculations for the positions and rotations of the passed agent
        /// * using the force to calculate in the <see cref="Calculate(SteeringAgent)"/>
        /// </summary>
        public void UpdateAgent(SteeringAgent _agent)
        {
            // do the calculating of the positions. No force calculations yet

            //* Calculate the new force and apply it to the agent.
            Vector3 force = Calculate(_agent);
            _agent.UpdateCurrentForce(force);

            //* Calculate the rotation using Slerp, The current rotation and the forcee for the target.

            Quaternion rotation = Quaternion.Slerp(
                _agent.Rotation,
                Quaternion.LookRotation(_agent.CurrentForce != Vector3.zero ? _agent.CurrentForce : _agent.Forward),
                Time.deltaTime * 10);

            // * Calculate the position by finding the corect movement then damping the difference

            Vector3 movement = (_agent.Forward + force * _agent.Speed) * Time.deltaTime;
            Vector3 position = Vector3.SmoothDamp(
                _agent.Position,
                movement + _agent.Position,
                ref _agent.velocity,
                _agent.MovementSmoothing);

            //* Apply the Calculated rotation and position
            _agent.ApplyPosAndRot(position, rotation);

        }


        /// <summary>
        ///* The function that the behaviours need to overide to calculate their forces for the agent.
        /// </summary>
        protected abstract Vector3 Calculate(SteeringAgent _agent);
        
    }
}
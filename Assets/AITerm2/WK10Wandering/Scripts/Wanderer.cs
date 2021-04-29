using UnityEngine;
//namespace is a way to group a code into it's own group.

//* Namespaces are like home addresses, anything under the namespace would live
//* in the house. The "root" namespace should always be the name of your game.
//* and each module of your game should hae it's own sub namespace; ie Enemies,
//* Towers, UI
namespace Wandering // a specific address, up until we use the namespace, c# wont know what to do if you're trying to use something from there.
{ 

    public class Wanderer : MonoBehaviour
    {
        [SerializeField, Range(.05f, .1f)] private float jitter = .05f; //need a jitter to manipulate our force
        [SerializeField, Min(1f)] private float speed = 1; // how much speed they have
        [SerializeField] private float smoothing = .1f; // trying to smooth the movement.

        //* force driving the agent. Updates every frame
        private Vector3 currentForce = Vector3.zero;

        //* The speed of the smoothed position is travelling
        private Vector3 velocity = Vector3.zero;




        // Update is called once per frame
        void Update()
        { // i mean you could do this first, then the calculate, but lesson went this after calculate
            //adding movement.


            //* Calculate the actual movement that needs to occur and how fast
            Vector3 movement = (transform.forward + (CalculateForce()* speed)) * Time.deltaTime;
            Vector3 position = Vector3.SmoothDamp(
                transform.position,
                transform.position + movement,
                ref velocity, // ref means referencing a particular part of the code
                smoothing);

            // calculate the rotation from where we are looking to the new one.
            Quaternion rotation = Quaternion.Slerp(
                transform.localRotation,
                Quaternion.LookRotation(currentForce),
                Time.deltaTime);

            // lerp, between two points in a linear way
            // slerp, projecting a point on a sphere, and the number given is that point.

            // applying position.
            transform.position = position;
            transform.rotation = rotation;
            // with this you've calculated steerring behaviour for Wandering.
        }

        /// <summary>
        /// Calculates the force the agent should be moving by. 
        /// Uses jitter to create the wandering effect.
        /// </summary>
        /// <returns></returns>
        private Vector3 CalculateForce()
        {
            //* first copy the current force and calculate the random offset using jitter.
            Vector3 force = currentForce;
            Vector2 offset = new Vector2(Random.Range(-jitter, jitter), Random.Range(-jitter, jitter));

            //* Add the offset to the horizontal and vertical axis of the transform
            // force is converted to a general direction
            force += transform.right * offset.x;
            force += transform.up * offset.y;

            //force itself is a direction it's going to move in. Doesn't have anything to do with speed.
            //now we need to convert it back to a direction by normalising it. (setting it back to one regardless of size)

            // * make sure the force is normalised because it is a direction

            force.Normalize();

            //* Update the current foce with the calculatied one and return it.
            currentForce = force;
            return force;

            // with this you have now calculated the direction the agent will move. now after this you need a step 
            // to actually make the agent move.

            // now this is when you go to void update to make movements.
        }

    }
}
using UnityEngine;
using System.Collections.Generic;

namespace Steering
{
    
    public static class SteeringAgentHelper
    {
        const int viewDirections = 100; // constant variables are just like static variables, but you can't change them during run time

        // read only means not modifyable on runtime. IT's only changeble by hard coding before build/play

        public static readonly Vector3[] directions; // changes array on points based on (?)
        private static Vector3[] coneDirections = null;

        // * Default parameters are parameters are parameters that don't need to specifically be passed in
        // * if they aren't, the set value will be used, otherwise the one passed will be.
        // * Default parameters also MUST be at the end of the parameter list
        public static Vector3[] DirectionsInCone(SteeringAgent _agent, bool _forceRecalculate = false) // force recalculate is a default param.
        {
            // * Determine if it has/nt been run before
            //until we run the calc, we have no idea how many directions are in the array.
            if(coneDirections == null || _forceRecalculate)
            {
                List<Vector3> newDirections = new List<Vector3>();

                // * loop through every direction that has been calculated in the sphere

                foreach (Vector3 direction in directions)
                {
                    // * Calculate the angle between the forward of the agent
                    // * and this direction, IF it is less than the view angle, 
                    // * we can add it to the list
                    if(Vector3.Angle(direction, _agent.Forward) < _agent.ViewAngle)
                    {
                        newDirections.Add(direction);
                    }

                }

                //* copy the directions found into the coneDirections array
                coneDirections = newDirections.ToArray();

            }


            return coneDirections;
        }

        // read only variables must be set -

        //constructor of a static class, will run the moment the code gets compiled/ runs the game.
        // is generated when it starts



        static SteeringAgentHelper()
        {
            directions = new Vector3[viewDirections];

            float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
            float angleIncrement = Mathf.PI * 2 * goldenRatio;

            for (int i = 0; i < viewDirections; i++)
            {
                float t = (float)i / viewDirections;
                float inclination = Mathf.Acos(1 - 2 * t);
                float azimuth = angleIncrement * i;

                float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
                float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
                float z = Mathf.Cos(inclination);

                directions[i] = new Vector3(x, y, z);
            }
        }

        
    }

}
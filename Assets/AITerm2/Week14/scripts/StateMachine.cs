using System.Collections.Generic;
using UnityEngine;


//Custom State Machines
namespace StateMachine
{
    // Enum to specify which state we're in
    public enum States
    {
        Translate,
        Rotate,
        Scale
    }

    // A delegate void with no parameters whatsover
    //* The delegate that dictates what the function for each state will look like.
    public delegate void StateDelegate();
    // That's all the variables data types we'll need

    public class StateMachine : MonoBehaviour
    {
        //private dictionary where the key will be the state and the value is State delegate
       /*# Is used to add states to the variable*/ private Dictionary<States, StateDelegate> states = new Dictionary<States, StateDelegate>();

       /*#*/ [SerializeField] private States currentState = States.Translate; // something to store what state it's currently in.
        
        [SerializeField] private Transform controlled; // * The thing that will be affected by our statemachine
        [SerializeField] private float speed = 1f; // * This isn't really in a statemachine, just for testing purposes
            
        // *This is used to change states from anywhere within the code that has reference
        // * tp tje state,acjome
        public void ChangeState(States _newState)
        {
            if(_newState != currentState)
            {
                currentState = _newState;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            // * This is the same as vchecking if the variable null, then setting otherwise
            // * retain the value
            controlled ??= transform;

            //# This uses the key from the dictionary "States" then calls the function 
            states.Add(States.Translate, Transform);
            states.Add(States.Rotate, Rotate);
            states.Add(States.Scale, Scale);

        }

        // Update is called once per frame
        void Update()
        {
            // after writing the functions, now we need to run the functions, which is why we had delegates

            //If the current state is in the dictionary, it retrieves the current state for the action
            // then runs that action

            // * These two lines are what actually runs the state machine.
            // * It works by attempting to retrieve the relevant function for the current state,
            // * Then running the function if it successfuly found it.
            if (states.TryGetValue(currentState, out StateDelegate state))
                state.Invoke();
            else
                Debug.LogError($"No state function set for state {currentState}");
        }

        // need to make functions for the states first and there's 3 of them

        //1st *The function that will run when we are in the Translate state.... meant to be named translate
        private void Transform() => controlled.position += transform.forward * Time.deltaTime * speed;
        //2nd- The rotate state
        private void Rotate() => controlled.Rotate(Vector3.up, speed * .5f);
        //3rd - The scale state
        private void Scale() => controlled.localScale += Vector3.one * Time.deltaTime * speed;

    }
}
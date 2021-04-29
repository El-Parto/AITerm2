using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Steering
{
    public class SteeringAgent : MonoBehaviour
    {
        // pointing to local position
        public Vector3 Position => transform.localPosition;
        public Quaternion Rotation => transform.localRotation;

        public Vector3 Forward => transform.forward;
        public Vector3 Right => transform.right;
        public Vector3 Up => transform.up;

        public Vector3 CurrentForce => currentForce; //same as force in the wanderer script.
        
        // not serialised, not going to save values, between playing
        [System.NonSerialized] public Vector3 velocity;
        // properties can't modify properties as references

        public float Speed => speed;
        public float MovementSmoothing => smoothing;


        [SerializeField, Range(.01f, .1f)] private float smoothing = .05f;
        [SerializeField] private new MeshRenderer renderer;
        [SerializeField] private SteeringBehaviour behaviour;


        private Vector3 currentForce;
        private float speed;


        public void Initialise(float _speed) => speed = _speed;
        public void UpdateAgent() => behaviour?.UpdateAgent(this); // "?" is like a null check. if it has a null it wont run anything beyond the .
        public void UpdateCurrentForce(Vector3 _force) => currentForce = _force;
        public void SetColor(Color _color) => renderer.material.color = _color;


        public void ApplyPosAndRot(Vector3 _pos, Quaternion _rot)
        {
            transform.localPosition = _pos;
            transform.localRotation = _rot;
        }
    
    }
}
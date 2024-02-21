using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Items
{
    public class ActivationItem : MonoBehaviour
    {
        private readonly static int Activate = Animator.StringToHash("Activate");
        [SerializeField] private float _activationRange;
        private float elapsedTimeSinceActivation = 0;
        private bool _isActive;
        
        public Animator Animator;

        private void Update()
        {
            TimeScale();
            Animator.SetBool(Activate, _isActive);
        }

        
        private void TimeScale()
        {
            elapsedTimeSinceActivation += Time.deltaTime;
            if ((elapsedTimeSinceActivation) >= _activationRange)
            {
                _isActive = !_isActive;
                elapsedTimeSinceActivation = 0;
            }
        }
    }
}
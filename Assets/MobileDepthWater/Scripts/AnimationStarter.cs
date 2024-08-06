namespace Assets.MobileOptimizedWater.Scripts
{
    using UnityEngine;

    public class AnimationStarter : MonoBehaviour
    {
        [SerializeField] private Animator animators;
        [SerializeField] private Motion animations;

        public void Awake()
        {
            animators.Play(animations.name);
        }
    }
}

using System.Collections.Generic;

namespace UnityTween
{
    public class TweenCoreManager : MonoSingleton<TweenCoreManager>
    {
        private List<TweenCore> _tweenCores = new List<TweenCore>();


        private void Update()
        {
            for (int i = _tweenCores.Count - 1; i >= 0; i--)
            {
                _tweenCores[i].Tick();
            }
        }

        public void AddTweenCore(TweenCore tweenCore)
        {
            if (_tweenCores.Contains(tweenCore)) return;
            _tweenCores.Add(tweenCore);
        }
        public void RemoveTweenCore(TweenCore tweenCore)
        {
            if (_tweenCores.Contains(tweenCore))
                _tweenCores.Remove(tweenCore);
        }

        public void RemoveAtTweenCore(int idx)
        {
            if (idx >= 0 && idx < _tweenCores.Count)
                _tweenCores.RemoveAt(idx);
        }
    }
}

using UnityEngine;
using System.Linq;
using System;

namespace UnityTween.TweenFactory
{
    public interface ITweenReflectionFactory // add polimorphism from tween factory??
    {
        Tween CreateTween(TweenOptionsReflection data);
    }

    public abstract class TweenReflectionFactory : ITweenReflectionFactory
    {
        public abstract Tween CreateTween(TweenOptionsReflection data);
    }

    public class TweenReflectionFloatFactory : TweenReflectionFactory
    {
        public override Tween CreateTween(TweenOptionsReflection data)
        {
            return null;
            //if (data.Target == null) return null;
            //var type = data.Type;
            //var test = data.Type.MakeGenericType(data.Type);
            //var secondList = Activator.CreateInstance(test);
            //var reference = data.Target.GetComponent<test>();
        }
    }
}
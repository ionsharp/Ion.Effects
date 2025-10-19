using Ion;
using Ion.Collect;
using Ion.Core;
using Ion.Reflect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ion.Effects;

public class EffectList : ListObservable<ImageEffect>
{
    public bool IsVisible { get => this.Get(true); set => this.Set(value); }

    public static IEnumerable<Type> Types
    {
        get
        {
            var result = new List<Type>();
            typeof(ImageEffect).Assembly.GetDerivedTypes<ImageEffect>(typeof(EffectList).Namespace).ForEach(result.Add);
            return result;
        }
    }

    public EffectList() : base() { }

    public EffectList(IEnumerable<ImageEffect> input) : base(input) { }

    public static IEnumerable<Type> GetTypes() => Types;
}

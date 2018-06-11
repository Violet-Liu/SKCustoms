using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class Maybe<T>
    {
        public abstract T Value { get; }
        public abstract bool HasValue { get; }

        public static implicit operator Maybe<T>(T value)
        {
            return (!typeof(T).IsValueType && Equals(null, value)) ? None<T>.Default : new Some<T>(value);
        }

        public Maybe<T> Do(Action<T> action)
        {
            if (HasValue)
                action(Value);
            return this;
        }

        //[DebuggerStepThrough]
        public Maybe<U> Select<U>(Func<T, Maybe<U>> func)
        {
            return this.HasValue ? func(this.Value) : None<U>.Default;
        }
        //[DebuggerStepThrough]
        public Maybe<V> Select<U, V>(Func<T, Maybe<U>> f, Func<T, U, V> g)
        {
            return Select(t => f(t).Select<V>(u => g(t, u)));
        }

        /// <summary>
        /// make sure this maybe object meet the predicate, or else None object will be returned
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        public Maybe<T> Where(Predicate<T> predicate)
        {
            if (this.HasValue && predicate(this.Value))
                return this;
            return None<T>.Default;
        }

        //[DebuggerStepThrough]
        public Maybe<T> DoWhen(Predicate<T> predicate, Action<T> action)
        {
            if (this.HasValue)
            {
                if (predicate(this.Value))
                    action(this.Value);
            }
            return this; ;
        }
        public Maybe<T> DoWhenOrElse(Predicate<T> predicate, Action<T> doWhen, Action<T> doElse)
        {
            if (this.HasValue)
            {
                if (predicate(this.Value))
                    doWhen(this.Value);
                else
                    doElse(this.Value);
            }
            return this;
        }
        public Maybe<U> FuncWhen<U>(Predicate<T> predicate, Func<T, U> reWhen)
        {
            if (this.HasValue)
            {
                if (predicate(this.Value))
                    return reWhen(this.Value);
            }
            return None<U>.Default;
        }

        public Maybe<T> DoIfNoValue(Action action)
        {
            if (!this.HasValue)
                action();
            return this;
        }

        public Maybe<U> ShiftWhenOrElse<U>(Predicate<T> predicate, Func<T, U> shiftWhen, Func<T, U> shiftElse)
        {
            if (this.HasValue)
            {
                if (predicate(this.Value))
                    return shiftWhen(this.Value);
                else
                    return shiftElse(this.Value);
            }
            return None<U>.Default;
        }

        /// <summary>
        /// execution method with double judgment.
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="outerpredicate">first judge</param>
        /// <param name="innerpredicate">second judge</param>
        /// <param name="otitShiftWhen">outerpredicate(true)=>innerpredicate(true) can func</param>
        /// <param name="otifShiftElse">outerpredicate(true)=>innerpredicate(false) can func</param>
        /// <param name="ofitShiftWhen">outerpredicate(true)=>innerpredicate(true) can func</param>
        /// <param name="ofifShiftElse">outerpredicate(true)=>innerpredicate(true) can func</param>
        /// <returns></returns>
        public Maybe<U> ShiftWhenOrElse<U>(Predicate<T> outerpredicate, Predicate<T> innerpredicate, Func<T, U> otitShiftWhen, Func<T, U> otifShiftElse, Func<T, U> ofitShiftWhen, Func<T, U> ofifShiftElse)
        {
            if (this.HasValue)
            {
                if (outerpredicate(this.Value))
                {
                    if (innerpredicate(this.Value))
                        return otitShiftWhen(this.Value);
                    else
                        return otifShiftElse(this.Value);
                }
                else
                {
                    if (innerpredicate(this.Value))
                        return ofitShiftWhen(this.Value);
                    else
                        return ofifShiftElse(this.Value);
                }

            }
            return None<U>.Default;
        }

        public Maybe<T> DoWhenButNone(Predicate<T> predicate, Action<T> action)
        {
            if (this.HasValue && predicate(this.Value))
            {
                action(this.Value);
                return None<T>.Default;
            }
            return this;
        }

    }

    public class Some<T> : Maybe<T>
    {
        private T _value;
        public override T Value { get { return _value; } }
        public override bool HasValue { get { return true; } }
        public Some(T t)
        {
            _value = t;
        }
    }
    public class None<T> : Maybe<T>
    {
        public static Maybe<T> Default = new None<T>();
        public override T Value
        {
            get
            {
                //return default(T);
                throw new InvalidOperationException("Do not have value");
            }
        }
        public override bool HasValue { get { return false; } }
    }
}

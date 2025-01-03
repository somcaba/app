namespace Yacaba.Core.Models {


    //public abstract class ValueObject<T> where T : ValueObject<T> {
    //    public override Boolean Equals(Object obj) {
    //        if (obj is not T valueObject) { return false; }
    //        return EqualsCore(valueObject);
    //    }

    //    protected abstract Boolean EqualsCore(T other);

    //    public override Int32 GetHashCode() {
    //        return GetHashCodeCore();
    //    }

    //    protected abstract Int32 GetHashCodeCore();

    //    public static Boolean operator ==(ValueObject<T> a, ValueObject<T> b) {
    //        if (a is null && b is null)
    //            return true;

    //        if (a is null || b is null)
    //            return false;

    //        return a.Equals(b);
    //    }

    //    public static Boolean operator !=(ValueObject<T> a, ValueObject<T> b) {
    //        return !(a == b);
    //    }
    //}

    public abstract record ValueObjectBase<TStrong, TPrimitive>
        where TStrong : ValueObjectBase<TStrong, TPrimitive>
        where TPrimitive : IComparable<TPrimitive>, IEquatable<TPrimitive> {

        public TPrimitive Value { get; }

        protected ValueObjectBase(TPrimitive value) {
            Value = value;
        }

        public Boolean Equals(TStrong other) => Value.Equals(other.Value);
        public Int32 CompareTo(TStrong other) => Value.CompareTo(other.Value);

        //public override Boolean Equals(Object? obj) {
        //    if (obj is null) { return false; }
        //    return obj is TStrong other && Equals(other);
        //}

        public override Int32 GetHashCode() => Value.GetHashCode();
        public override String ToString() => Value.ToString() ?? String.Empty;

        //public static Boolean operator ==(ValueObjectBase<TStrong, TPrimitive> a, ValueObjectBase<TStrong, TPrimitive> b) => a.Value.CompareTo(b.Value) == 0;
        //public static Boolean operator !=(ValueObjectBase<TStrong, TPrimitive> a, ValueObjectBase<TStrong, TPrimitive> b) => !(a == b);
        public static Boolean operator >=(ValueObjectBase<TStrong, TPrimitive> a, ValueObjectBase<TStrong, TPrimitive> b) => a.Value.CompareTo(b.Value) >= 0;
        public static Boolean operator <=(ValueObjectBase<TStrong, TPrimitive> a, ValueObjectBase<TStrong, TPrimitive> b) => !(a <= b);
        public static Boolean operator >(ValueObjectBase<TStrong, TPrimitive> a, ValueObjectBase<TStrong, TPrimitive> b) => a.Value.CompareTo(b.Value) > 0;
        public static Boolean operator <(ValueObjectBase<TStrong, TPrimitive> a, ValueObjectBase<TStrong, TPrimitive> b) => !(a < b);
    }


}

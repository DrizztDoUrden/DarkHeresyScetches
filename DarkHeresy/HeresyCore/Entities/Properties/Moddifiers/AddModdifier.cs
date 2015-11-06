using HeresyCore.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HeresyCore.Entities.Properties.Moddifiers
{
    public abstract class AddModdifier<TContent> : PropertyModdifier<TContent>
    {
        public TContent Value { get; set; }

        public AddModdifier(TContent value)
        {
            Value = value;
        }

        private static IDictionary<Type, Func<TContent, object>> _moddifierConstructors = new Dictionary<Type, Func<TContent, object>>(new TypeComparer())
        {
            [typeof(byte)]      = value => new ByteAddModdifier     ((byte)     (object)value),

            [typeof(short)]     = value => new ShortAddModdifier    ((short)    (object)value),
            [typeof(ushort)]    = value => new UshortAddModdifier   ((ushort)   (object)value),

            [typeof(int)]       = value => new IntAddModdifier      ((int)      (object)value),
            [typeof(uint)]      = value => new UintAddModdifier     ((uint)     (object)value),

            [typeof(long)]      = value => new LongAddModdifier     ((long)     (object)value),
            [typeof(ulong)]     = value => new UlongAddModdifier    ((ulong)    (object)value),

            [typeof(float)]     = value => new FloatAddModdifier    ((float)    (object)value),
            [typeof(double)]    = value => new DoubleAddModdifier   ((double)   (object)value),

            [typeof(string)]    = value => new StringAddModdifier   ((string)   (object)value),
        };

        public static AddModdifier<TContent> Create(TContent value)
        {
            Func<TContent, object> factory;

            if (!_moddifierConstructors.TryGetValue(typeof(TContent), out factory))
                throw new NotImplementedException($"Моддификатора свойств типа <{typeof(TContent).AssemblyQualifiedName}> не существует");

            return (AddModdifier<TContent>)factory(value);
        }

        [DebuggerStepThrough]
        public static implicit operator AddModdifier<TContent>(TContent value) => Create(value);
    }

    public sealed class ByteAddModdifier : AddModdifier<byte>
    {
        [DebuggerStepThrough]
        public ByteAddModdifier(byte value) : base(value) { }

        [DebuggerStepThrough]
        public static implicit operator ByteAddModdifier(byte value) => new ByteAddModdifier(value);

        public override byte Moddify(byte baseValue, ICollection<string> conditions)
            => (byte)(baseValue + Value);
    }

    public sealed class ShortAddModdifier : AddModdifier<short>
    {
        [DebuggerStepThrough]
        public ShortAddModdifier(short value) : base(value) { }

        [DebuggerStepThrough]
        public static implicit operator ShortAddModdifier(short value) => new ShortAddModdifier(value);

        public override short Moddify(short baseValue, ICollection<string> conditions)
            => (short)(baseValue + Value);
    }

    public sealed class UshortAddModdifier : AddModdifier<ushort>
    {
        [DebuggerStepThrough]
        public UshortAddModdifier(ushort value) : base(value) { }

        [DebuggerStepThrough]
        public static implicit operator UshortAddModdifier(ushort value) => new UshortAddModdifier(value);

        public override ushort Moddify(ushort baseValue, ICollection<string> conditions)
            => (ushort)(baseValue + Value);
    }

    public sealed class IntAddModdifier : AddModdifier<int>
    {
        [DebuggerStepThrough]
        public IntAddModdifier(int value) : base(value) { }

        [DebuggerStepThrough]
        public static implicit operator IntAddModdifier(int value) => new IntAddModdifier(value);

        public override int Moddify(int baseValue, ICollection<string> conditions)
            => baseValue + Value;
    }

    public sealed class UintAddModdifier : AddModdifier<uint>
    {
        [DebuggerStepThrough]
        public UintAddModdifier(uint value) : base(value) { }

        [DebuggerStepThrough]
        public static implicit operator UintAddModdifier(uint value) => new UintAddModdifier(value);

        public override uint Moddify(uint baseValue, ICollection<string> conditions)
            => baseValue + Value;
    }

    public sealed class UlongAddModdifier : AddModdifier<ulong>
    {
        [DebuggerStepThrough]
        public UlongAddModdifier(ulong value) : base(value) { }

        [DebuggerStepThrough]
        public static implicit operator UlongAddModdifier(ulong value) => new UlongAddModdifier(value);

        public override ulong Moddify(ulong baseValue, ICollection<string> conditions)
            => baseValue + Value;
    }

    public sealed class LongAddModdifier : AddModdifier<long>
    {
        [DebuggerStepThrough]
        public LongAddModdifier(long value) : base(value) { }

        [DebuggerStepThrough]
        public static implicit operator LongAddModdifier(long value) => new LongAddModdifier(value);

        public override long Moddify(long baseValue, ICollection<string> conditions)
            => baseValue + Value;
    }

    public sealed class DoubleAddModdifier : AddModdifier<double>
    {
        [DebuggerStepThrough]
        public DoubleAddModdifier(double value) : base(value) { }

        [DebuggerStepThrough]
        public static implicit operator DoubleAddModdifier(double value) => new DoubleAddModdifier(value);

        public override double Moddify(double baseValue, ICollection<string> conditions)
            => baseValue + Value;
    }

    public sealed class FloatAddModdifier : AddModdifier<float>
    {
        [DebuggerStepThrough]
        public FloatAddModdifier(float value) : base(value) { }

        [DebuggerStepThrough]
        public static implicit operator FloatAddModdifier(float value) => new FloatAddModdifier(value);

        public override float Moddify(float baseValue, ICollection<string> conditions)
            => baseValue + Value;
    }

    public sealed class StringAddModdifier : AddModdifier<string>
    {
        [DebuggerStepThrough]
        public StringAddModdifier(string value) : base(value) { }

        [DebuggerStepThrough]
        public static implicit operator StringAddModdifier(string value) => new StringAddModdifier(value);

        public override string Moddify(string baseValue, ICollection<string> conditions)
            => baseValue + Value;
    }
}

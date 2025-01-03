using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yacaba.Domain.Models {
    internal class _noworking {
    }

    //[ModelBinder(typeof(OrganisationIdBinder))]
    //public record OrganisationId(Int64 Value) {
    //    public static implicit operator Int64(OrganisationId src) => src.Value;
    //    public static implicit operator OrganisationId(Int64 src) => new OrganisationId(src);

    //    public static Boolean operator >=(OrganisationId a, OrganisationId b) => a.Value.CompareTo(b.Value) >= 0;
    //    public static Boolean operator <=(OrganisationId a, OrganisationId b) => !(a.Value <= b.Value);
    //    public static Boolean operator >(OrganisationId a, OrganisationId b) => a.Value.CompareTo(b.Value) > 0;
    //    public static Boolean operator <(OrganisationId a, OrganisationId b) => !(a.Value < b.Value);
    //}

    ////public class OrganisationIdConverter : JsonConverter<OrganisationId> {
    ////    public override OrganisationId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    ////    public override void Write(Utf8JsonWriter writer, OrganisationId value, JsonSerializerOptions options) => throw new NotImplementedException();
    ////}

    //public class OrganisationIdBinder : IModelBinder {

    //    public Task BindModelAsync(ModelBindingContext bindingContext) {
    //        ArgumentNullException.ThrowIfNull(bindingContext);

    //        String modelName = bindingContext.ModelName;
    //        ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

    //        if (valueProviderResult == ValueProviderResult.None) { return Task.CompletedTask; }

    //        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

    //        String? value = valueProviderResult.FirstValue;
    //        if (String.IsNullOrEmpty(value)) { return Task.CompletedTask; }

    //        if (Int64.TryParse(value, out Int64 id) == false) {
    //            bindingContext.ModelState.TryAddModelError(modelName, "OrganisationId must be an Int64.");
    //            return Task.CompletedTask;
    //        }

    //        var model = new OrganisationId(id);
    //        bindingContext.Result = ModelBindingResult.Success(model);
    //        return Task.CompletedTask;
    //    }
    //}

    //[TypeConverter(typeof(OrganisationIdTypeConverter))]
    //public record OrganisationId(Int64 Value) : IConvertible {

    //    public static implicit operator Int64(OrganisationId src) => src.Value;
    //    public static implicit operator OrganisationId(Int64 src) => new OrganisationId(src);
    //    //public static explicit operator OrganisationId(Int64 src) => new OrganisationId(src);

    //    public static Boolean operator >=(OrganisationId a, OrganisationId b) => a.Value.CompareTo(b.Value) >= 0;
    //    public static Boolean operator <=(OrganisationId a, OrganisationId b) => !(a.Value <= b.Value);
    //    public static Boolean operator >(OrganisationId a, OrganisationId b) => a.Value.CompareTo(b.Value) > 0;
    //    public static Boolean operator <(OrganisationId a, OrganisationId b) => !(a.Value < b.Value);

    //    public static Boolean operator ==(OrganisationId a, Int64 b) => a.Value.CompareTo(b) == 0;
    //    public static Boolean operator !=(OrganisationId a, Int64 b) => !(a.Value == b);
    //    public static Boolean operator >=(OrganisationId a, Int64 b) => a.Value.CompareTo(b) >= 0;
    //    public static Boolean operator <=(OrganisationId a, Int64 b) => !(a.Value <= b);
    //    public static Boolean operator >(OrganisationId a, Int64 b) => a.Value.CompareTo(b) > 0;
    //    public static Boolean operator <(OrganisationId a, Int64 b) => !(a.Value < b);

    //    public TypeCode GetTypeCode() => TypeCode.Object;
    //    public Boolean ToBoolean(IFormatProvider? provider) => Convert.ToBoolean(Value);
    //    public Byte ToByte(IFormatProvider? provider) => Convert.ToByte(Value);
    //    public Char ToChar(IFormatProvider? provider) => Convert.ToChar(Value);
    //    public DateTime ToDateTime(IFormatProvider? provider) => Convert.ToDateTime(Value);
    //    public Decimal ToDecimal(IFormatProvider? provider) => Convert.ToDecimal(Value);
    //    public Double ToDouble(IFormatProvider? provider) => Convert.ToDouble(Value);
    //    public Int16 ToInt16(IFormatProvider? provider) => Convert.ToInt16(Value);
    //    public Int32 ToInt32(IFormatProvider? provider) => Convert.ToInt32(Value);
    //    public Int64 ToInt64(IFormatProvider? provider) => Convert.ToInt64(Value);
    //    public SByte ToSByte(IFormatProvider? provider) => Convert.ToSByte(Value);
    //    public Single ToSingle(IFormatProvider? provider) => Convert.ToSingle(Value);
    //    public String ToString(IFormatProvider? provider) => $"{Value}";
    //    public Object ToType(Type conversionType, IFormatProvider? provider) => Convert.ChangeType(Value, conversionType, provider);
    //    public UInt16 ToUInt16(IFormatProvider? provider) => Convert.ToUInt16(Value); 
    //    public UInt32 ToUInt32(IFormatProvider? provider) => Convert.ToUInt32(Value);
    //    public UInt64 ToUInt64(IFormatProvider? provider) => Convert.ToUInt64(Value);

    //}

    //public class OrganisationIdTypeConverter : TypeConverter {
    //    public override Boolean CanConvertFrom(ITypeDescriptorContext? context, Type sourceType) {
    //        return sourceType == typeof(Int64) || base.CanConvertFrom(context, sourceType);
    //    }

    //    public override Boolean CanConvertTo(ITypeDescriptorContext? context, [NotNullWhen(true)] Type? destinationType) {
    //        return destinationType == typeof(Int64) || base.CanConvertTo(context, destinationType);
    //    }

    //    public override Object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, Object value) {
    //        if (value is Int64 casted) { return new OrganisationId(casted); }
    //        return base.ConvertFrom(context, culture, value);
    //    }

    //    public override Object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, Object? value, Type destinationType) {
    //        if (value is OrganisationId casted) { return casted.Value; }
    //        return base.ConvertTo(context, culture, value, destinationType);
    //    }
    //}

}

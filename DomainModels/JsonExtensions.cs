using System.Text.Json;
using System.Globalization;

public static class JsonExtensions
{
    public static string RequireString(JsonElement el, string name)
    {
        if(!el.TryGetProperty(name, out var prop))
        {
            throw new InvalidOperationException($"Dados faltando: '{name}'");
        }

        var value = prop.GetString();

        // if(string.IsNullOrWhiteSpace(value))
        // {
        //     throw new InvalidOperationException($"Propriedade '{name}' está vazia");
        // }

        if(value == null)
        {
            throw new InvalidOperationException($"Propriedade '{name}' está vazia");
        }
        return value;
    }

    public static decimal RequireDecimal(JsonElement el, string name)
    {
        if(!el.TryGetProperty(name, out var prop))
        {
            throw new InvalidOperationException($"Dados faltando: '{name}'");
        }

        if(prop.ValueKind == JsonValueKind.Number)
        {
            return prop.GetDecimal();
        }

        if(prop.ValueKind == JsonValueKind.String && decimal.TryParse(prop.GetString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
        {
            return d;
        }

        throw new InvalidOperationException($"Numero inválido '{name}'");
    }

    public static ulong RequireUInt64(JsonElement el, string name)
    {
        if(!el.TryGetProperty(name, out var prop))
        {
            throw new InvalidOperationException($"Dados faltando: '{name}'");
        }

        if(prop.ValueKind == JsonValueKind.Number)
        {
            return prop.GetUInt64();
        }

        throw new InvalidOperationException($"Número inválido '{name}'");
    }

    public static string RequireModoEntrega(JsonElement shippingLines)
    {
        if(shippingLines.ValueKind != JsonValueKind.Array || shippingLines.GetArrayLength() == 0)
        {
            throw new InvalidOperationException("Modo de entrega não enviado");
        }

        var methodId = shippingLines[0].GetProperty("method_id").GetString();

        if(methodId == "pickup_location")
        {
            return "1";
        }
        else if(methodId == "flat_rate" || methodId == "free_shipping")
        {
            //return ModoEntrega.Entrega;
            return "3";
        }
        else
        {
            throw new InvalidOperationException($"Modo de entrega inválido {methodId}");
        }
    }

    public static string? GetStringSafe(this JsonElement element, string name)
    {
        if(!element.TryGetProperty(name, out var prop))
        {
            return null;
        }

        return prop.ValueKind == JsonValueKind.String ? prop.GetString() : null;
    } 

    public static decimal? GetDecimalSafe(this JsonElement element, string name)
    {
        if(!element.TryGetProperty(name, out var prop))
        {
            return null;
        }

        if(prop.ValueKind == JsonValueKind.Number)
        {
            return prop.GetDecimal();
        }

        if(prop.ValueKind == JsonValueKind.String && decimal.TryParse(prop.GetString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
        {
            return d;
        }

        return null;
    }

    public static ulong? GetUInt64Safe(this JsonElement element, string name)
    {
        if(!element.TryGetProperty(name, out var prop))
        {
            return null;
        }

        return prop.ValueKind == JsonValueKind.Number ? prop.GetUInt64() : null;
    }
}
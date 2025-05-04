namespace StationDevice.DataContracts;

/// <summary>
/// A Weather Forecast for a specific date
/// </summary>
/// <param name="Date">Gets the Date of the Forecast.</param>
/// <param name="TemperatureC">Gets the Forecast Temperature in Celsius.</param>
/// <param name="Summary">Get a description of how the weather will feel.</param>
//public record WeatherForecast(DateOnly Date, double TemperatureC, string? Summary)
//{
//    /// <summary>
//    /// Gets the Forecast Temperature in Fahrenheit
//    /// </summary>
//    public double TemperatureF => 32 + (TemperatureC * 9 / 5);
//}





public class ColorPallete
{
    public string description { get; set; }
    public string seed { get; set; }
    public Corecolors coreColors { get; set; }
    public object[] extendedColors { get; set; }
    public Schemes schemes { get; set; }
    public Palettes palettes { get; set; }
}

public class Corecolors
{
    public string primary { get; set; }
    public string secondary { get; set; }
    public string tertiary { get; set; }
}

public class Schemes
{
    public Light light { get; set; }
    public LightMediumContrast lightmediumcontrast { get; set; }
    public LightHighContrast lighthighcontrast { get; set; }
    public Dark dark { get; set; }
    public DarkMediumContrast darkmediumcontrast { get; set; }
    public DarkHighContrast darkhighcontrast { get; set; }
}

public class Light
{
    public string primary { get; set; }
    public string surfaceTint { get; set; }
    public string onPrimary { get; set; }
    public string primaryContainer { get; set; }
    public string onPrimaryContainer { get; set; }
    public string secondary { get; set; }
    public string onSecondary { get; set; }
    public string secondaryContainer { get; set; }
    public string onSecondaryContainer { get; set; }
    public string tertiary { get; set; }
    public string onTertiary { get; set; }
    public string tertiaryContainer { get; set; }
    public string onTertiaryContainer { get; set; }
    public string error { get; set; }
    public string onError { get; set; }
    public string errorContainer { get; set; }
    public string onErrorContainer { get; set; }
    public string background { get; set; }
    public string onBackground { get; set; }
    public string surface { get; set; }
    public string onSurface { get; set; }
    public string surfaceVariant { get; set; }
    public string onSurfaceVariant { get; set; }
    public string outline { get; set; }
    public string outlineVariant { get; set; }
    public string shadow { get; set; }
    public string scrim { get; set; }
    public string inverseSurface { get; set; }
    public string inverseOnSurface { get; set; }
    public string inversePrimary { get; set; }
    public string primaryFixed { get; set; }
    public string onPrimaryFixed { get; set; }
    public string primaryFixedDim { get; set; }
    public string onPrimaryFixedVariant { get; set; }
    public string secondaryFixed { get; set; }
    public string onSecondaryFixed { get; set; }
    public string secondaryFixedDim { get; set; }
    public string onSecondaryFixedVariant { get; set; }
    public string tertiaryFixed { get; set; }
    public string onTertiaryFixed { get; set; }
    public string tertiaryFixedDim { get; set; }
    public string onTertiaryFixedVariant { get; set; }
    public string surfaceDim { get; set; }
    public string surfaceBright { get; set; }
    public string surfaceContainerLowest { get; set; }
    public string surfaceContainerLow { get; set; }
    public string surfaceContainer { get; set; }
    public string surfaceContainerHigh { get; set; }
    public string surfaceContainerHighest { get; set; }
}

public class LightMediumContrast
{
    public string primary { get; set; }
    public string surfaceTint { get; set; }
    public string onPrimary { get; set; }
    public string primaryContainer { get; set; }
    public string onPrimaryContainer { get; set; }
    public string secondary { get; set; }
    public string onSecondary { get; set; }
    public string secondaryContainer { get; set; }
    public string onSecondaryContainer { get; set; }
    public string tertiary { get; set; }
    public string onTertiary { get; set; }
    public string tertiaryContainer { get; set; }
    public string onTertiaryContainer { get; set; }
    public string error { get; set; }
    public string onError { get; set; }
    public string errorContainer { get; set; }
    public string onErrorContainer { get; set; }
    public string background { get; set; }
    public string onBackground { get; set; }
    public string surface { get; set; }
    public string onSurface { get; set; }
    public string surfaceVariant { get; set; }
    public string onSurfaceVariant { get; set; }
    public string outline { get; set; }
    public string outlineVariant { get; set; }
    public string shadow { get; set; }
    public string scrim { get; set; }
    public string inverseSurface { get; set; }
    public string inverseOnSurface { get; set; }
    public string inversePrimary { get; set; }
    public string primaryFixed { get; set; }
    public string onPrimaryFixed { get; set; }
    public string primaryFixedDim { get; set; }
    public string onPrimaryFixedVariant { get; set; }
    public string secondaryFixed { get; set; }
    public string onSecondaryFixed { get; set; }
    public string secondaryFixedDim { get; set; }
    public string onSecondaryFixedVariant { get; set; }
    public string tertiaryFixed { get; set; }
    public string onTertiaryFixed { get; set; }
    public string tertiaryFixedDim { get; set; }
    public string onTertiaryFixedVariant { get; set; }
    public string surfaceDim { get; set; }
    public string surfaceBright { get; set; }
    public string surfaceContainerLowest { get; set; }
    public string surfaceContainerLow { get; set; }
    public string surfaceContainer { get; set; }
    public string surfaceContainerHigh { get; set; }
    public string surfaceContainerHighest { get; set; }
}

public class LightHighContrast
{
    public string primary { get; set; }
    public string surfaceTint { get; set; }
    public string onPrimary { get; set; }
    public string primaryContainer { get; set; }
    public string onPrimaryContainer { get; set; }
    public string secondary { get; set; }
    public string onSecondary { get; set; }
    public string secondaryContainer { get; set; }
    public string onSecondaryContainer { get; set; }
    public string tertiary { get; set; }
    public string onTertiary { get; set; }
    public string tertiaryContainer { get; set; }
    public string onTertiaryContainer { get; set; }
    public string error { get; set; }
    public string onError { get; set; }
    public string errorContainer { get; set; }
    public string onErrorContainer { get; set; }
    public string background { get; set; }
    public string onBackground { get; set; }
    public string surface { get; set; }
    public string onSurface { get; set; }
    public string surfaceVariant { get; set; }
    public string onSurfaceVariant { get; set; }
    public string outline { get; set; }
    public string outlineVariant { get; set; }
    public string shadow { get; set; }
    public string scrim { get; set; }
    public string inverseSurface { get; set; }
    public string inverseOnSurface { get; set; }
    public string inversePrimary { get; set; }
    public string primaryFixed { get; set; }
    public string onPrimaryFixed { get; set; }
    public string primaryFixedDim { get; set; }
    public string onPrimaryFixedVariant { get; set; }
    public string secondaryFixed { get; set; }
    public string onSecondaryFixed { get; set; }
    public string secondaryFixedDim { get; set; }
    public string onSecondaryFixedVariant { get; set; }
    public string tertiaryFixed { get; set; }
    public string onTertiaryFixed { get; set; }
    public string tertiaryFixedDim { get; set; }
    public string onTertiaryFixedVariant { get; set; }
    public string surfaceDim { get; set; }
    public string surfaceBright { get; set; }
    public string surfaceContainerLowest { get; set; }
    public string surfaceContainerLow { get; set; }
    public string surfaceContainer { get; set; }
    public string surfaceContainerHigh { get; set; }
    public string surfaceContainerHighest { get; set; }
}

public class Dark
{
    public string primary { get; set; }
    public string surfaceTint { get; set; }
    public string onPrimary { get; set; }
    public string primaryContainer { get; set; }
    public string onPrimaryContainer { get; set; }
    public string secondary { get; set; }
    public string onSecondary { get; set; }
    public string secondaryContainer { get; set; }
    public string onSecondaryContainer { get; set; }
    public string tertiary { get; set; }
    public string onTertiary { get; set; }
    public string tertiaryContainer { get; set; }
    public string onTertiaryContainer { get; set; }
    public string error { get; set; }
    public string onError { get; set; }
    public string errorContainer { get; set; }
    public string onErrorContainer { get; set; }
    public string background { get; set; }
    public string onBackground { get; set; }
    public string surface { get; set; }
    public string onSurface { get; set; }
    public string surfaceVariant { get; set; }
    public string onSurfaceVariant { get; set; }
    public string outline { get; set; }
    public string outlineVariant { get; set; }
    public string shadow { get; set; }
    public string scrim { get; set; }
    public string inverseSurface { get; set; }
    public string inverseOnSurface { get; set; }
    public string inversePrimary { get; set; }
    public string primaryFixed { get; set; }
    public string onPrimaryFixed { get; set; }
    public string primaryFixedDim { get; set; }
    public string onPrimaryFixedVariant { get; set; }
    public string secondaryFixed { get; set; }
    public string onSecondaryFixed { get; set; }
    public string secondaryFixedDim { get; set; }
    public string onSecondaryFixedVariant { get; set; }
    public string tertiaryFixed { get; set; }
    public string onTertiaryFixed { get; set; }
    public string tertiaryFixedDim { get; set; }
    public string onTertiaryFixedVariant { get; set; }
    public string surfaceDim { get; set; }
    public string surfaceBright { get; set; }
    public string surfaceContainerLowest { get; set; }
    public string surfaceContainerLow { get; set; }
    public string surfaceContainer { get; set; }
    public string surfaceContainerHigh { get; set; }
    public string surfaceContainerHighest { get; set; }
}

public class DarkMediumContrast
{
    public string primary { get; set; }
    public string surfaceTint { get; set; }
    public string onPrimary { get; set; }
    public string primaryContainer { get; set; }
    public string onPrimaryContainer { get; set; }
    public string secondary { get; set; }
    public string onSecondary { get; set; }
    public string secondaryContainer { get; set; }
    public string onSecondaryContainer { get; set; }
    public string tertiary { get; set; }
    public string onTertiary { get; set; }
    public string tertiaryContainer { get; set; }
    public string onTertiaryContainer { get; set; }
    public string error { get; set; }
    public string onError { get; set; }
    public string errorContainer { get; set; }
    public string onErrorContainer { get; set; }
    public string background { get; set; }
    public string onBackground { get; set; }
    public string surface { get; set; }
    public string onSurface { get; set; }
    public string surfaceVariant { get; set; }
    public string onSurfaceVariant { get; set; }
    public string outline { get; set; }
    public string outlineVariant { get; set; }
    public string shadow { get; set; }
    public string scrim { get; set; }
    public string inverseSurface { get; set; }
    public string inverseOnSurface { get; set; }
    public string inversePrimary { get; set; }
    public string primaryFixed { get; set; }
    public string onPrimaryFixed { get; set; }
    public string primaryFixedDim { get; set; }
    public string onPrimaryFixedVariant { get; set; }
    public string secondaryFixed { get; set; }
    public string onSecondaryFixed { get; set; }
    public string secondaryFixedDim { get; set; }
    public string onSecondaryFixedVariant { get; set; }
    public string tertiaryFixed { get; set; }
    public string onTertiaryFixed { get; set; }
    public string tertiaryFixedDim { get; set; }
    public string onTertiaryFixedVariant { get; set; }
    public string surfaceDim { get; set; }
    public string surfaceBright { get; set; }
    public string surfaceContainerLowest { get; set; }
    public string surfaceContainerLow { get; set; }
    public string surfaceContainer { get; set; }
    public string surfaceContainerHigh { get; set; }
    public string surfaceContainerHighest { get; set; }
}

public class DarkHighContrast
{
    public string primary { get; set; }
    public string surfaceTint { get; set; }
    public string onPrimary { get; set; }
    public string primaryContainer { get; set; }
    public string onPrimaryContainer { get; set; }
    public string secondary { get; set; }
    public string onSecondary { get; set; }
    public string secondaryContainer { get; set; }
    public string onSecondaryContainer { get; set; }
    public string tertiary { get; set; }
    public string onTertiary { get; set; }
    public string tertiaryContainer { get; set; }
    public string onTertiaryContainer { get; set; }
    public string error { get; set; }
    public string onError { get; set; }
    public string errorContainer { get; set; }
    public string onErrorContainer { get; set; }
    public string background { get; set; }
    public string onBackground { get; set; }
    public string surface { get; set; }
    public string onSurface { get; set; }
    public string surfaceVariant { get; set; }
    public string onSurfaceVariant { get; set; }
    public string outline { get; set; }
    public string outlineVariant { get; set; }
    public string shadow { get; set; }
    public string scrim { get; set; }
    public string inverseSurface { get; set; }
    public string inverseOnSurface { get; set; }
    public string inversePrimary { get; set; }
    public string primaryFixed { get; set; }
    public string onPrimaryFixed { get; set; }
    public string primaryFixedDim { get; set; }
    public string onPrimaryFixedVariant { get; set; }
    public string secondaryFixed { get; set; }
    public string onSecondaryFixed { get; set; }
    public string secondaryFixedDim { get; set; }
    public string onSecondaryFixedVariant { get; set; }
    public string tertiaryFixed { get; set; }
    public string onTertiaryFixed { get; set; }
    public string tertiaryFixedDim { get; set; }
    public string onTertiaryFixedVariant { get; set; }
    public string surfaceDim { get; set; }
    public string surfaceBright { get; set; }
    public string surfaceContainerLowest { get; set; }
    public string surfaceContainerLow { get; set; }
    public string surfaceContainer { get; set; }
    public string surfaceContainerHigh { get; set; }
    public string surfaceContainerHighest { get; set; }
}

public class Palettes
{
    public Primary primary { get; set; }
    public Secondary secondary { get; set; }
    public Tertiary tertiary { get; set; }
    public Neutral neutral { get; set; }
    public NeutralVariant neutralvariant { get; set; }
}

public class Primary
{
    public string _0 { get; set; }
    public string _5 { get; set; }
    public string _10 { get; set; }
    public string _15 { get; set; }
    public string _20 { get; set; }
    public string _25 { get; set; }
    public string _30 { get; set; }
    public string _35 { get; set; }
    public string _40 { get; set; }
    public string _50 { get; set; }
    public string _60 { get; set; }
    public string _70 { get; set; }
    public string _80 { get; set; }
    public string _90 { get; set; }
    public string _95 { get; set; }
    public string _98 { get; set; }
    public string _99 { get; set; }
    public string _100 { get; set; }
}

public class Secondary
{
    public string _0 { get; set; }
    public string _5 { get; set; }
    public string _10 { get; set; }
    public string _15 { get; set; }
    public string _20 { get; set; }
    public string _25 { get; set; }
    public string _30 { get; set; }
    public string _35 { get; set; }
    public string _40 { get; set; }
    public string _50 { get; set; }
    public string _60 { get; set; }
    public string _70 { get; set; }
    public string _80 { get; set; }
    public string _90 { get; set; }
    public string _95 { get; set; }
    public string _98 { get; set; }
    public string _99 { get; set; }
    public string _100 { get; set; }
}

public class Tertiary
{
    public string _0 { get; set; }
    public string _5 { get; set; }
    public string _10 { get; set; }
    public string _15 { get; set; }
    public string _20 { get; set; }
    public string _25 { get; set; }
    public string _30 { get; set; }
    public string _35 { get; set; }
    public string _40 { get; set; }
    public string _50 { get; set; }
    public string _60 { get; set; }
    public string _70 { get; set; }
    public string _80 { get; set; }
    public string _90 { get; set; }
    public string _95 { get; set; }
    public string _98 { get; set; }
    public string _99 { get; set; }
    public string _100 { get; set; }
}

public class Neutral
{
    public string _0 { get; set; }
    public string _5 { get; set; }
    public string _10 { get; set; }
    public string _15 { get; set; }
    public string _20 { get; set; }
    public string _25 { get; set; }
    public string _30 { get; set; }
    public string _35 { get; set; }
    public string _40 { get; set; }
    public string _50 { get; set; }
    public string _60 { get; set; }
    public string _70 { get; set; }
    public string _80 { get; set; }
    public string _90 { get; set; }
    public string _95 { get; set; }
    public string _98 { get; set; }
    public string _99 { get; set; }
    public string _100 { get; set; }
}

public class NeutralVariant
{
    public string _0 { get; set; }
    public string _5 { get; set; }
    public string _10 { get; set; }
    public string _15 { get; set; }
    public string _20 { get; set; }
    public string _25 { get; set; }
    public string _30 { get; set; }
    public string _35 { get; set; }
    public string _40 { get; set; }
    public string _50 { get; set; }
    public string _60 { get; set; }
    public string _70 { get; set; }
    public string _80 { get; set; }
    public string _90 { get; set; }
    public string _95 { get; set; }
    public string _98 { get; set; }
    public string _99 { get; set; }
    public string _100 { get; set; }
}


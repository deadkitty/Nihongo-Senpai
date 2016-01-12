using SenpaiCreationKit.Resources;

namespace SenpaiCreationKit
{
    /// <summary>
    /// Bietet Zugriff auf Zeichenfolgenressourcen.
    /// </summary>
    public class LocalizedStrings
    {
        private static AppResources _localizedResources = new AppResources();

        public AppResources Resources { get { return _localizedResources; } }
    }
}
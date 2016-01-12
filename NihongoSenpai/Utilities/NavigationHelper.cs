
namespace NihongoSenpai.Utilities
{
    public class NavigationHelper
    {
        private static object navigationParameter;

        public static object NavigationParameter
        {
            get { return NavigationHelper.navigationParameter; }
            set { NavigationHelper.navigationParameter = value; }
        }
    }
}

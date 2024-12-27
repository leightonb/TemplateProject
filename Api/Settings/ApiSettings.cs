using System.Text;

namespace TemplateProject.Settings;
internal static class ApiSettings
{
    internal static string SecretKey = "a9lmahpeymv7rp07w1m64lx90z6wrke4";
    internal static byte[] GenerateSecretByte() =>
        Encoding.ASCII.GetBytes(SecretKey);
}
namespace EmbreeSharp
{
    public static class InteropUtlity
    {
        public static unsafe long Strlen(byte* s)
        {
            byte* sc;
            for (sc = s; *sc != '\0'; ++sc) ;
            return sc - s;
        }
    }
}

namespace EmbreeSharp.SIMD.Test;

internal static class TestHelpers
{
    public static unsafe int AlignedOffset<TScalar>(TScalar* ptr, uint alignmentMask, int laneCount) where TScalar : unmanaged
    {
        for (int i = 0; i < laneCount; i++)
        {
            if ((((nuint)(ptr + i)) & alignmentMask) == 0)
            {
                return i;
            }
        }

        Assert.Fail($"Could not find a {alignmentMask + 1}-byte aligned {typeof(TScalar).Name} lane.");
        return 0;
    }

    public static void ExpectArgumentException(Action action)
    {
        try
        {
            action();
        }
        catch (ArgumentException)
        {
            return;
        }

        Assert.Fail("Expected an ArgumentException.");
    }
}

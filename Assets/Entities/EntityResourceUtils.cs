using Utils;

public static class EntityResourceUtils 
{
    public static void LosePercentageResource (Resource<float> resource, float value)
    {
        Utils.Utils.SetAndClampFloatResource(resource, resource.CurrentValue.PresentValue * (1.0f - value));
    }
}

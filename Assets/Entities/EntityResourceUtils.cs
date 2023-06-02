using Utils;

public static class EntityResourceUtils 
{
    public static void LosePercentageMaxResource (Resource<float> resource, float value)
    {
        Utils.Utils.SetAndClampFloatResource(resource, resource.CurrentValue.PresentValue - (resource.MaxValue.PresentValue * (1.0f - value)));
    }
    public static void RegainPercentageMaxResource (Resource<float> resource, float value)
    {
        Utils.Utils.SetAndClampFloatResource(resource, resource.CurrentValue.PresentValue + (resource.MaxValue.PresentValue * value));
    }
}

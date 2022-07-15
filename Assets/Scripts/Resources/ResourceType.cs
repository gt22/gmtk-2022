namespace Resources
{
    public enum ResourceType
    {
        MATERIALS,
        MANPOWER,
        SUPPLIES
    }

    public static class ResourceTypeMethods
    {
        public static ResourceStack Stack(this ResourceType type, int amount)
        {
            var res = new ResourceStack();
            res[type] = amount;
            return res;
        }
    }
}
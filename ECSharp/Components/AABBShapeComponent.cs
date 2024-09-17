class AABBShapeComponent : IEntityComponent
{
    // vector2f for the upper of the AABB
    public float UpperX;
    public float UpperY;

    // vector2f for the lower of the AABB
    public float LowerX;
    public float LowerY;

    // vector2f for the hitbox of the AABB
    public float HitboxX;
    public float HitboxY;
}
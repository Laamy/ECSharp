using System;
using System.Collections.Generic;
using System.Linq;

class MyEntity
{
    public EntityContext context;

    public MyEntity(SimpleRegistry Registry)
    {
        this.context = new EntityContext(Registry);
    }

    public bool IsOnGround()
    {
        return context.HasComponent<FlagComponent<OnGroundFlag>>();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // registry for our entities
        SimpleRegistry registry = new SimpleRegistry();

        // some entities
        List<MyEntity> entities = new List<MyEntity>();

        // setup some basic example entities
        for (int i = 0; i < 10; ++i)
        {
            MyEntity entity = new MyEntity(registry);
            entities.Add(entity);

            registry.Emplace(entity.context.EntityId, new StateVectorComponent() {
                VelocityX = 1,
                VelocityY = 2,

                PrevVelocityX = 3,
                PrevVelocityY = 4
            });

            registry.Emplace(entity.context.EntityId, new AABBShapeComponent()
            {
                HitboxX = 2,
                HitboxY = 2,

                LowerX = 4,
                LowerY = 4,

                UpperY = 6,
                UpperX = 6
            });

            registry.Emplace(entity.context.EntityId, new FlagComponent<OnGroundFlag>()); // set onground flag
        }

        // get just the StateVectorComponent's
        foreach (var (entityId, stateVec) in registry.GetComponents<StateVectorComponent>())
        {
            bool isOnGround = registry.HasComponent<FlagComponent<OnGroundFlag>>(entityId);

            Console.WriteLine($"[{entityId}] {stateVec.VelocityX},{stateVec.VelocityY}");
            Console.WriteLine($"[{entityId}] IsOnground: {isOnGround}");
        }

        // or lets say you wanted to get a simple component from a single entity
        {
            var entity = entities[3];

            var stateVec = entity.context.TryGetComponent<StateVectorComponent>();

            Console.WriteLine("Single entity stuff");
            Console.WriteLine($"[{entity.context.EntityId}] {stateVec.VelocityX},{stateVec.VelocityY}");
            Console.WriteLine($"[{entity.context.EntityId}] IsOnground: {entity.IsOnGround()}");
        }

        Console.ReadKey();
    }
}
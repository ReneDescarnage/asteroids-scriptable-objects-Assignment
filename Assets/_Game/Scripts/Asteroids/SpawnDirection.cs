using System;

namespace Asteroids {
    [Flags]
    public enum SpawnDirection
    {

        Top = 1<<0,
        Bottom= 1<<1,
        Left= 1<<2,
        Right= 1<<3
    }
}

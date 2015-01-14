﻿using UnityEngine;
using System.Collections;

public class MissileSprite : ScratchySprite
{
    public float Speed = 200;
    public GameObject ExplosionType;

    public override void OnStart()
    {
        Wait(5, Destroy);
    }

    public override void OnUpdate()
    {
        // Move the missile
        Y = Y + Speed * Time.deltaTime;

        // Collide with aliens
        var deadAlien = GetTouchingSprite<AlienSprite>();
        if (deadAlien != null)
        {
            Clone(ExplosionType, deadAlien.transform.position);
            
            var pixels = deadAlien.ConvertToPixelQuads(true, true, 50);
            deadAlien.Destroy();
            Wait(3, () =>
            {
                foreach (var pixel in pixels)
                {
                    Destroy(pixel);
                }
                this.Destroy();
            });
        }
    }
}

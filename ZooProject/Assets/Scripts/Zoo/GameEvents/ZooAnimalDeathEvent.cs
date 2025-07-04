﻿using System.Collections;
using System;

namespace Zoo
{
    public class ZooAnimalDeathEvent : IZooAnimalEvent
    {
        public readonly EZooAnimalDietaryType Dietary;

        public ZooAnimalDeathEvent(EZooAnimalDietaryType dietary)
        {
            Dietary = dietary;
        }

        public override string ToString()
        {
            return $"({base.ToString()}){{{nameof(Dietary)}: {Dietary}}}";
        }
    }
}
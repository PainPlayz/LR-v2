﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wServer.logic.behaviors;
using wServer.logic.behaviors.PetBehaviors;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Pets = () => LoadPetBehaviors();

        private static ctor LoadPetBehaviors()
        {
            ctor ret = Behav();
            foreach (var item in InitGameData.TypeToPet)
            {
                ret.Init(item.Value.ObjectId,
                    new State(
                        new PetFollow(),
                        new Prioritize(
                            new StayCloseToSpawn(0.2),
                            new PetWander(0.1, new Cooldown(10000, 10000))
                        ),
                        new PetHealHP(),
                        new PetHealMP(),
                        new PetElectric(),
                        new PetShoot(),
                        new PetRisingFury(),
                        new PetSavage()
                    )
                );
            }
            return ret;
        }
    }
}

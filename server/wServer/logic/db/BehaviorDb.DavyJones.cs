﻿#region

using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;
using wServer.realm;

#endregion

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ DavyJones = () => Behav()
        .Init("Davy Jones",
                new State(                    
                    new State("Floating",
                        new ChangeSize(100, 100),
                        new SetAltTexture(1),
                        new SetAltTexture(3),
                        new Wander(.2),
                        new Shoot(10, 5, 10, 0, coolDown: 2000),
                        new Shoot(10, 1, 10, 1, coolDown: 4000),
                        new EntityNotExistsTransition("Ghost Lanturn Off", 10, "Vunerable"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable)
                        ),
                    new State("CheckOffLanterns",
                        new SetAltTexture(2),
                        new StayCloseToSpawn(.1, 3),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new EntityNotExistsTransition("Ghost Lanturn Off", 10, "Vunerable")
                        ),
                    new State("Vunerable",
                        new SetAltTexture(2),
                        new StayCloseToSpawn(.1, 0),
                        new TimedTransition(2500, "deactivate")
                        ),
                    new State("deactivate",
                        new SetAltTexture(2),
                        new StayCloseToSpawn(.1, 0),
                        new EntityNotExistsTransition("Ghost Lanturn On", 10, "Floating")
                        )
                    ),
                new MostDamagers(5,
                    new ItemLoot("Potion of Wisdom", 1),
                    new ItemLoot("Potion of Attack", 0.5)
                    ),
                new MostDamagers(3,
                    new OnlyOne(
                        new ItemLoot("Spirit Dagger", whitebag),
                        new ItemLoot("Spectral Cloth Armor", greatloot),
                        new ItemLoot("Ghostly Prism", whitebag),
                        new ItemLoot("Captain's Ring", normalloot)
                        ),
                    new ItemLoot("Wine Cellar Incantation", winecellar),
                    new ItemLoot("Spirit Egg", eggbag + normalloot),
                    new ItemLoot("Ruby Gemstone", awesomeloot),
                    new ItemLoot("Golden Chalice", awesomeloot),
                    new ItemLoot("Pearl Necklace", awesomeloot),
                    new TierLoot(4, ItemType.Ring, normalloot),
                    new TierLoot(3, ItemType.Ability, mediumloot),
                    new TierLoot(4, ItemType.Ability, normalloot),
                    new TierLoot(5, ItemType.Ability, goodloot),
                    new TierLoot(8, ItemType.Armor, mediumloot),
                    new TierLoot(9, ItemType.Armor, mediumloot),
                    new TierLoot(8, ItemType.Weapon, mediumloot),
                    new TierLoot(9, ItemType.Weapon, normalloot),
                    new TierLoot(10, ItemType.Weapon, goodloot)
                    )
            /*
            Davy Jones (Prod Loot):
            -- init white bag loot
            - spirit dagger
            - ghostly prism
            -- end white bag loot

            -- init pet bag
            - spirit egg
            -- end pet bag

            -- init treasure loot
            - wine cellar incantation
            - golden chalice
            - pearl necklace
            - ruby gemstone
            -- end treasure loot

            -- init pot bag
            - att
            - wis
            -- end pot bag

            -- init normal loot
            - t5 ability
            - t10 weapon
            - t4 ring
            - t4 ability
            - t9 armor
            - t9 weapon
            - t8 weapon
            - t8 armor
            - t3 ability
            -- end normal loot

            -- init additional loot
            - ghost pirate rum
            -- end additional loot
            */
            )
        .Init("Ghost Lanturn Off",
            new State(
                new State("default",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new EntityNotExistsTransition("Yellow Key", 9999, "gogogo")
                        ),
                new State("gogogo",
                    new TransformOnDeath("Ghost Lanturn On")
                    )
                    )
            )
            .Init("GhostShip PurpleDoor Rt",
                new State(
                    new State("Idle",
                        new EntityNotExistsTransition("Purple Key", 500, "Cycle")

                    ),
                    new State("Cycle",
                        new PlayerWithinTransition(2, "Cycle2")

                    ),
                    new State("Cycle2",
                        new Decay(1000)
                    )
               //248, 305
               )
            )
            .Init("GhostShip PurpleDoor Lf",
                new State(
                    new State("Idle",
                        new EntityNotExistsTransition("Purple Key", 500, "Cycle")

                    ),
                    new State("Cycle",
                        new PlayerWithinTransition(2, "Cycle2")

                    ),
                    new State("Cycle2",
                        new Decay(1000)
                    )
               //248, 305
               )
            )
            .Init("Lost Soul",
                new State(
                    new State("Default",
                        new Prioritize(
                            new Orbit(0.3, 3, 20, "Ghost of Roger"),
                            new Wander(0.1)
                            ),
                        new PlayerWithinTransition(4, "Default1")
                        ),
                    new State("Default1",
                       new Charge(0.5, 8, coolDown: 2000),
                       new TimedTransition(2200, "Blammo")
                        ),
                     new State("Blammo",
                       new Shoot(10, count: 6, projectileIndex: 0, coolDown: 2000),
                       new Suicide()
                    )
                )
            ).Init("Ghost of Roger",
                new State(
                    new State("spawn",
                        new Spawn("Lost Soul", 3, 1, 5000),
                        new TimedTransition(100, "Attack")
                    ),
                    new State("Attack",
                        new Shoot(13, 1, 0, 0, coolDown: 10),
                        new TimedTransition(20, "Attack2")
                    ),
                    new State("Attack2",
                        new Shoot(13, 1, 0, 0, coolDown: 10),
                        new TimedTransition(20, "Attack3")
                    ),
                    new State("Attack3",
                        new Shoot(13, 1, 0, 0, coolDown: 10),
                        new TimedTransition(20, "Wait")
                    ),
                    new State("Wait",
                        new TimedTransition(1000, "Attack")
                    )
                )
            )
            .Init("GhostShip GreenDoor Rt",
                new State(
                    new State("Idle",
                        new EntityNotExistsTransition("Green Key", 500, "Cycle")

                    ),
                    new State("Cycle",
                        new PlayerWithinTransition(2, "Cycle2")

                    ),
                    new State("Cycle2",
                        new Decay(1000)
                    )
               //248, 305
               )
            )
            .Init("GhostShip GreenDoor Lf",
                new State(
                    new State("Idle",
                        new EntityNotExistsTransition("Green Key", 500, "Cycle")

                    ),
                    new State("Cycle",
                        new PlayerWithinTransition(2, "Cycle2")

                    ),
                    new State("Cycle2",
                        new Decay(1000)
                    )
               //248, 305
               )
            )
            .Init("GhostShip YellowDoor Rt",
                new State(
                    new State("Idle",
                        new EntityNotExistsTransition("Yellow Key", 500, "Cycle")

                    ),
                    new State("Cycle",
                        new PlayerWithinTransition(2, "Cycle2")

                    ),
                    new State("Cycle2",
                        new Decay(1000)
                    )
               //248, 305
               )
            )
            .Init("GhostShip YellowDoor Lf",
                new State(
                    new State("Idle",
                        new EntityNotExistsTransition("Yellow Key", 500, "Cycle")

                    ),
                    new State("Cycle",
                        new PlayerWithinTransition(2, "Cycle2")

                    ),
                    new State("Cycle2",
                        new Decay(1000)
                    )
               //248, 305
               )
            )
            .Init("GhostShip RedDoor Rt",
                new State(
                    new State("Idle",
                        new EntityNotExistsTransition("Red Key", 500, "Cycle")

                    ),
                    new State("Cycle",
                        new PlayerWithinTransition(2, "Cycle2")

                    ),
                    new State("Cycle2",
                        new Decay(1000)
                    )
               //248, 305
               )
            )
            .Init("GhostShip RedDoor Lf",
                new State(
                    new State("Idle",
                        new EntityNotExistsTransition("Red Key", 500, "Cycle")

                    ),
                    new State("Cycle",
                        new PlayerWithinTransition(2, "Cycle2")

                    ),
                    new State("Cycle2",
                        new Decay(1000)
                    )
               //248, 305
               )
            )
            .Init("Purple Key",
                new State(
                    new State("Idle",
                        new PlayerWithinTransition(1, "Cycle")

                    ),
                    new State("Cycle",
                        new Taunt(true, "Purple Key has been found!"),
                        new Decay(200)



                    )
                )
            )
            .Init("Red Key",
                new State(
                    new State("Idle",
                        new PlayerWithinTransition(1, "Cycle")

                    ),
                    new State("Cycle",
                        new Taunt(true, "Red Key has been found!"),
                        new Decay(200)



                    )
                )
            )
            .Init("Green Key",
                new State(
                    new State("Idle",
                        new PlayerWithinTransition(1, "Cycle")

                    ),
                    new State("Cycle",
                        new Taunt(true, "Green Key has been found!"),
                        new Decay(200)



                    )
                )
            )
            .Init("Yellow Key",
                new State(
                    new State("Idle",
                        new PlayerWithinTransition(1, "Cycle")

                    ),
                    new State("Cycle",
                        new Taunt(true, "Yellow Key has been found!"),
                        new Decay(200)



                    )
                )
            )
            .Init("Ghost Lanturn On",
                new State(
                    new State("idle",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new TimedTransition(5000, "deactivate")
                        ),
                    new State("deactivate",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new EntityNotExistsTransition("Ghost Lanturn Off", 10, "shoot"),
                        new TimedTransition(10000, "gone")
                        ),
                    new State("shoot",
                        new Shoot(10, 6, coolDown: 9000001, coolDownOffset: 100),
                        new TimedTransition(1000, "gone")
                        ),
                    new State("gone",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Transform("Ghost Lanturn Off")
                        )
                    )
            )
  .Init("Lil' Ghost Pirate",
                new State(
                    new ChangeSize(30, 120),
                    new Shoot(10, count: 1, projectileIndex: 0, coolDown: 2000),
                    new State("Default",
                        new Prioritize(
                            new Follow(0.6, 8, 1),
                            new Wander(0.1)
                            ),
                        new TimedTransition(2850, "Default1")
                        ),
                    new State("Default1",
                       new StayBack(0.2, 3),
                       new TimedTransition(1850, "Default")
                    )
                )
            )
                 .Init("Zombie Pirate Sr",
                new State(
                    new Shoot(10, count: 1, projectileIndex: 0, coolDown: 2000),
                    new State("Default",
                        new Prioritize(
                            new Follow(0.3, 8, 1),
                            new Wander(0.1)
                            ),
                        new TimedTransition(2850, "Default1")
                        ),
                    new State("Default1",
                       new ConditionalEffect(ConditionEffectIndex.Armored),
                       new Prioritize(
                            new Follow(0.3, 8, 1),
                            new Wander(0.1)
                            ),
                        new TimedTransition(2850, "Default")
                    )
                )
            )
           .Init("Zombie Pirate Jr",
                new State(
                    new Shoot(10, count: 1, projectileIndex: 0, coolDown: 2500),
                    new State("Default",
                        new Prioritize(
                            new Follow(0.4, 8, 1),
                            new Wander(0.1)
                            ),
                        new TimedTransition(2850, "Default1")
                        ),
                    new State("Default1",
                       new Swirl(0.2, 3),
                       new TimedTransition(1850, "Default")
                    )
                )
            )
        .Init("Captain Summoner",
                new State(
                    new State("Default",
                        new ConditionalEffect(ConditionEffectIndex.Invincible)
                        )
                )
            )
           .Init("GhostShip Rat",
                new State(
                    new State("Default",
                        new Shoot(10, count: 1, projectileIndex: 0, coolDown: 1750),
                        new Prioritize(
                            new Follow(0.55, 8, 1),
                            new Wander(0.1)
                            )
                        )
                )
            )
        .Init("Violent Spirit",
                new State(
                    new State("Default",
                        new ChangeSize(35, 120),
                        new Shoot(10, count: 3, projectileIndex: 0, coolDown: 1750),
                        new Prioritize(
                            new Follow(0.25, 8, 1),
                            new Wander(0.1)
                            )
                        )
                )
            )
           .Init("School of Ghostfish",
                new State(
                    new State("Default",
                        new Shoot(10, count: 3, shootAngle: 18, projectileIndex: 0, coolDown: 4000),
                        new Wander(0.35)
                        )
                )
            );
    }
}
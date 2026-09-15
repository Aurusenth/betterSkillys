using Shared.resources;
using WorldServer.logic.behaviors;
using WorldServer.logic.loot;
using WorldServer.logic.transitions;
using WorldServer.logic.behaviors.@new.movements;
namespace WorldServer.logic
{
    partial class BehaviorDb
    {
        private _ Feral = () => Behav()
       .Init("F.E.R.A.L.",
           new State(
               new HpLessTransition(0.01, "Death"),
               new State("1",
                   new ConditionEffectBehavior(ConditionEffectIndex.Invincible),
                   new Taunt("Hello, idk what behaviors i am normally on but im just trying out some random shit"),
                   new TimedTransition(3000, "2")
                   ),
               new State("2",
                   new ConditionEffectBehavior(ConditionEffectIndex.Invincible),
                   new Taunt("Lets begin our journey together"),
                   new TimedTransition(3000, "3")
                   ),
               new State("3",
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 0, coolDown: 1200),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 15, coolDown: 1200, coolDownOffset: 200),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 30, coolDown: 1200, coolDownOffset: 400),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 45, coolDown: 1200, coolDownOffset: 600),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 60, coolDown: 1200, coolDownOffset: 800),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 75, coolDown: 1200, coolDownOffset: 1000),
                   new TimedTransition(1200, "4")
                   ),
               new State("4",
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 0, coolDown: 1200),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 15, coolDown: 1200, coolDownOffset: 200),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 30, coolDown: 1200, coolDownOffset: 400),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 45, coolDown: 1200, coolDownOffset: 600),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 60, coolDown: 1200, coolDownOffset: 800),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 75, coolDown: 1200, coolDownOffset: 1000),
                   new TimedTransition(1200, "5")
                   ),
               new State("5",
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 0, coolDown: 1200),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 15, coolDown: 1200, coolDownOffset: 200),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 30, coolDown: 1200, coolDownOffset: 400),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 45, coolDown: 1200, coolDownOffset: 600),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 60, coolDown: 1200, coolDownOffset: 800),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 75, coolDown: 1200, coolDownOffset: 1000),
                   new TimedTransition(1200, "a")
                   ),
               new State("a",
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 0, coolDown: 1200),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 15, coolDown: 1200, coolDownOffset: 200),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 30, coolDown: 1200, coolDownOffset: 400),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 45, coolDown: 1200, coolDownOffset: 600),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 60, coolDown: 1200, coolDownOffset: 800),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 75, coolDown: 1200, coolDownOffset: 1000),
                   new TimedTransition(1200, "b")
                   ),
               new State("b",
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 0, coolDown: 1200),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 15, coolDown: 1200, coolDownOffset: 200),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 30, coolDown: 1200, coolDownOffset: 400),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 45, coolDown: 1200, coolDownOffset: 600),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 60, coolDown: 1200, coolDownOffset: 800),
                   new Shoot(0, projectileIndex: 1, count: 6, shootAngle: 60, fixedAngle: 75, coolDown: 1200, coolDownOffset: 1000),
                   new TimedTransition(1200, "6")
                   ),
               new State("6",
                   new ConditionEffectBehavior(ConditionEffectIndex.Invincible),
                   new Taunt("Enough of this boring shit, lets try something else."),
                   new TimedTransition(3000, "7")
                   ),
               new State("7",
                       new Shoot(20, 5, 1, 1, coolDown: 1000),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 200),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 400),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 600),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 800),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 1000),
                       new TimedTransition(1000, "8")
                   ),
               new State("8",
                       new Shoot(20, 5, 1, 1, coolDown: 1000),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 200),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 400),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 600),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 800),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 1000),
                       new TimedTransition(1000, "9")
                   ),
               new State("9",
                       new Shoot(20, 5, 1, 1, coolDown: 1000),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 200),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 400),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 600),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 800),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 1000),
                       new TimedTransition(1000, "q")
                   ),
               new State("q",
                       new Shoot(20, 5, 1, 1, coolDown: 1000),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 200),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 400),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 600),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 800),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 1000),
                       new TimedTransition(1000, "plz")
                   ),
               new State("plz",
                       new Shoot(20, 5, 1, 1, coolDown: 1000),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 200),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 400),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 600),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 800),
                       new Shoot(20, 5, 1, 1, coolDown: 1000, coolDownOffset: 1000),
                       new TimedTransition(1000, "10")
                   ),
               new State("10",
                   new ConditionEffectBehavior(ConditionEffectIndex.Invincible),
                   new Taunt("Meh, i guess i am really stupid. I am not supposed to even be in this game."),
                   new TimedTransition(3000, "TalkAgain")
                   ),
               new State("TalkAgain",
                   new ConditionEffectBehavior(ConditionEffectIndex.Invincible),
                   new Taunt("Oh well, guess im just gonna follow you, and shoot like a weird guy on drugs"),
                   new TimedTransition(3000, "11")
                   ),
               new State("11",
                   new Follow(0.8, range: 1),
                   new Shoot(50, 25, projectileIndex: 0, coolDown: 50),
                   new TimedTransition(5000, "12")
                   ),
               new State("12",
                   new ConditionEffectBehavior(ConditionEffectIndex.Invincible),
                   new Taunt("Nevermind actually, let me try and spawn something"),
                   new TimedTransition(3000, "Pause1")
                   ),
               new State("Pause1",
                   new ConditionEffectBehavior(ConditionEffectIndex.Invincible),
                   new Taunt("Lets start off with some Mini bots!"),
                   new TimedTransition(3000, "SpawnShit")
                   ),
               new State("SpawnShit",
                   new ConditionEffectBehavior(ConditionEffectIndex.Invincible),
                   new Spawn("Mini Bot", 5),
                   new TimedTransition(1000, "CheckSpawnShit")
                   ),
               new State("Pause2",
                   new ConditionEffectBehavior(ConditionEffectIndex.Invincible),
                   new Taunt("Lets spawn a few Enforcer Bots 3000!"),
                   new TimedTransition(3000, "SpawnShit2")
                   ),
               new State("SpawnShit2",
                   new ConditionEffectBehavior(ConditionEffectIndex.Invincible),
                   new Spawn("Enforcer Bot 3000", 3),
                   new TimedTransition(1000, "CheckSpawnShit2")
                   ),
               new State("Pause3",
                   new ConditionEffectBehavior(ConditionEffectIndex.Invincible),
                   new Taunt("Final Spawn, Dr Terrible! Gl!"),
                   new TimedTransition(3000, "SpawnShit3")
                   ),
               new State("SpawnShit3",
                   new ConditionEffectBehavior(ConditionEffectIndex.Invincible),
                   new Spawn("Dr Terrible", 1),
                   new TimedTransition(1000, "CheckSpawnShit3")
                   ),
               new State("CheckSpawnShit",
                   new ConditionEffectBehavior(ConditionEffectIndex.Invincible),
                   new EntityNotExistsTransition("Mini Bot", 500, "Pause2")
                   ),
               new State("CheckSpawnShit2",
                   new ConditionEffectBehavior(ConditionEffectIndex.Invincible),
                   new EntityNotExistsTransition("Enforcer Bot 3000", 500, "Pause3")
                   ),
               new State("CheckSpawnShit3",
                   new ConditionEffectBehavior(ConditionEffectIndex.Invincible),
                   new EntityNotExistsTransition("Dr Terrible", 500, "PreDeath")
                   ),
               new State("PreDeath",
                   new ConditionEffectBehavior(ConditionEffectIndex.Invulnerable),
                   new Taunt("Well i got nothing more for you here. Thanks for playing with me tho, have been fun!"),
                   new TimedTransition(5000, "Death")
                   ),
               new State("Death",
                   new ConditionEffectBehavior(ConditionEffectIndex.Invulnerable),
                   new Taunt("Byebye, have fun!"),
                   new TimedTransition(3000, "Suicide")
                   ),
               new State("Suicide",
                   new Suicide()
                   )
               )
           );
    }
}

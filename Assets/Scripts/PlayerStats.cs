public class PlayerStats
{
    //You need 2 dice to play

    //Choose Warrior Abilities between Kyujutsu, Iaijutsu, Karumijutsu ou Ni-to-Kenjutsu (Can influence mission results)
    /* Kyujutsu (Bow and Arrow)
    - 12 Arrows
        - 3 Folha de Salgueiro (+2 damage points)
        - 3 Gut Drillers (+2 pointes)
        - 3 Armor Drillers (+2 points, can only be used against certain enemies)
        - 3 Sibilantes (+1 and make a scary noise)

    If able to shoot an arrow choose which type to shoot.
    You can get lucky and get back some arrows, or find new ones.

    To see if you hit the target you have to throw the dices - The result needs to less than your Strength.
     */

    /* Iaijutsu (Quick Draw)
    
    You start combat by quickly shooting one arrow to the enemy on the first attack. Gaining +3 Strength
     */

    /* Karumijutsu (Heroic Jump)
     
    You're an incredible acrobat, you'll be allowed to use this on special occasions.
     */

    /* Ni-to-Kenjutsu
     You fight with 2 swords, a Wakizashi short-sword and a Katana. All samurai bring both swords with them but not all can use them at the same time.
    For that if you attack a enemy with 9 or more dice points, you attack other time before the enemy can defend or counter-attack. (if the second attack is also >9 you can't attack a third-time)
     */ 

    public int Honor = 3; //Can be won and lost through the gameplay

    /* Honor points
     You start with 3 points of Honor
    If your Honor ever drops down to 0 you have to go to Action 99.
    Your honor determines storylines you can follow or not.
     */

    //You have a sword and a bagpack with food and drink for your journey

    //These three can change through the gameplay - But they are capped to their initialation value
    public int Strength = 6; //Starts with 6 + roll of one dice - Ability and Combate Technique
    public int Health = 12; //Starts with 12 + roll of two dices - Conditioning, Instinct, Will and Determination
    public int Luck = 6; //Starts with 6 + roll of one dice - Lucky or not

    public int Meals = 10;



    /*
     * Combate Tutorial:
     * 1- Throw 2 dices for the enemy + its current Strength - That's its Attack Power
     * 2- Throw 2 dices for you + current Strength - That's your Attack Power
     * 3- If your attack power is higher than the monster you hurt it. Follow to Step 4. Otherwise go to Step 5. If the attack powers are the same Start from 1 again.
     * 4- You hurt the monster take 2 points to its Strenght you can use Luck to strike it more.
     * 5- You are hurt take 2 points out of your Strenght. You can use Luck here.
     * 6- Update fighters scores. If Luck was used do adjustments.
     * 7- Repeat until one of the fighters score is <= 0
     * 
     * Against multiple enemies:
     * 1- Choose the one to fight first, follow the combat 1o1 rules.
     * 2- While fighting one, roll the dice for your fight with the other enemies. Isn't of hurting of getting hurt, you can only defend or get hurt depending on Power Attacks.
     * 
     * Luck:
     * Luck options appear throughout the gameplay. If so, roll the dices.
     * 1- If the roll is <= to your Luck you are lucky. If not you have a penalty.
     * 2- Each time you use your Luck, deduce one point.
     * 
     * In Battle you can use the luck to hurt more or less (+2/-1) or defend more or less (-2/+1).
     * Through the game you can gain more luck points.
     * 
     * 
     * Your bagpack has enough supplies for 10 meals, you can stop and rest whenever you want, expect in combat.
     * Each time you stop to rest you gain +4 Health and -1 Meal.
     */


}

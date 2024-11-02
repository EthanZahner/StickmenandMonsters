using System;

[System.Serializable]
public partial struct DiceRoll
{

    public int count;
    public int sides;
    public int bonus;


    public DiceRoll(int sides)
    {
        this.count = 1;
        this.sides = sides;
        this.bonus = 0;
    }

    public DiceRoll(int count, int sides)
    {
        this.count = count;
        this.sides = sides;
        this.bonus = 0;

    }

    public DiceRoll(int count, int sides, int bonus)
    {
        this.count = count;
        this.sides = sides;
        this.bonus = bonus;

    }

    public static readonly DiceRoll D4 = new(4);
    public static readonly DiceRoll D6 = new(6);
    public static readonly DiceRoll D8 = new(8);
    public static readonly DiceRoll D10 = new(10);
    public static readonly DiceRoll D12 = new(12);
    public static readonly DiceRoll D20 = new(20);
    public static readonly DiceRoll D100 = new(100);
    
}

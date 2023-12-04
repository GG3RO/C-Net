Attack Attack1 = new Attack("Attack1", 30);
Attack Attack2 = new Attack("Attack2", 50);
Attack Attack3 = new Attack("Attack3",10);
System.Console.WriteLine(Attack1.DamageAmount );

Enemy opponent1 = new Enemy("opponent1");
Enemy  opponent2 = new Enemy("opponent2");
opponent1.AttacksList.Add(Attack1);
opponent1.AttacksList.Add(Attack2);
opponent1.AttacksList.Add(Attack3);
opponent1.RandomAattackEnemy(opponent2);
opponent1.RandomAattackEnemy(opponent2);

    // create class attack 
public class Attack{
    
    public string Name;
    public int DamageAmount;

    public Attack(string NameValue,int DamageValue  ){
        Name=NameValue;
        DamageAmount=DamageValue;

    }
}

public class Enemy{
    public string  Name;
    public int health = 100;
    public List<Attack> AttacksList=new List<Attack>();

        public Enemy(string vleraName){
            Name=vleraName;

        }

    public void RandomAattack(){
        Random rand=new Random();
        int RandomPosition = rand.Next(AttacksList.Count);
        Attack RandomAtt= AttacksList[RandomPosition];
        System.Console.WriteLine($"attack with {RandomAtt.Name} with damage {RandomAtt.DamageAmount}");
    }

    public void RandomAattackEnemy(Enemy enemy){
        Random rand=new Random();
        int  RandomPosition = rand.Next(AttacksList.Count);
        Attack RandomAtt= AttacksList[RandomPosition];
        enemy.health -= RandomAtt.DamageAmount;
        System.Console.WriteLine($"attack with {RandomAtt.Name} the enemy {enemy.Name} with damage {RandomAtt.DamageAmount} and he is left with {enemy.health} health ");
    }

}
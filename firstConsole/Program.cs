//Three arrays
int[] myIntArray =new int[9];
for(int i =0; i<9; i++){
    myIntArray[i]=i;
}

string[] nameArray={"tim", "Martin", "Nikki","Sara"};

bool[]  array= new bool[10];
for(int i=1; i<=10; i++){
    if(i% 2 != 0){
        System.Console.WriteLine("True");

    }
    else{
        System.Console.WriteLine("Fasle");
    }
}
// //List of FLavors 

List<string> iceCream= new List<string>(){
    ("Pistachio") ,("Strawberry"), (" Chocolate Chip"),("Rocky Road"),("Coffee")

};
System.Console.WriteLine(iceCream.Count);
System.Console.WriteLine(iceCream[2]);
iceCream.RemoveAt(2);
System.Console.WriteLine(iceCream.Count);


//Dictionary

Dictionary<string,string> profile = new Dictionary<string,string>();

Random rand = new Random();
foreach(string name in nameArray){
    int randomIndex= rand.Next(iceCream.Count);
    string randomFlavor=iceCream[randomIndex];
    profile.Add(name, randomFlavor);

}
foreach(var entry in profile){
    System.Console.WriteLine($"{entry.Key} favourite flavor is{entry.Value}.");
}













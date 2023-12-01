static void PrintList(List<string> MyList)
{
    foreach(string item in MyList){
        System.Console.WriteLine(item);

    };
}
List<string> TestStringList = new List<string>() {"Harry", "Steve", "Carla", "Jeanne"};
PrintList(TestStringList);

static void SumOfNumbers(List<int> IntList)
{
    int sum=0;
    foreach(int number in IntList){
        sum+= number;
        System.Console.WriteLine(sum);

    }
    
}
List<int> TestIntList = new List<int>() {2,7,12,9,3};
// You should get back 33 in this example
SumOfNumbers(TestIntList);

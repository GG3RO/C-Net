// iterate and print
static void PrintList(List<string> MyList)
{
    foreach(string item in MyList){
        System.Console.WriteLine(item);
    };
}
List<string> TestStringList = new List<string>() {"Harry", "Steve", "Carla", "Jeanne"};
PrintList(TestStringList);

//print sum
static void SumOfNumbers(List<int> IntList)
{
    int sum=0;
    foreach(int number in IntList){
        sum= sum + number;
    }
    System.Console.WriteLine(sum);
}
List<int> TestIntList = new List<int>() {2,7,12,9,3};
// You should get back 33 in this example
SumOfNumbers(TestIntList);

// find max

static int FindMax(List<int> IntList)
{
    int Max = IntList[0];
    foreach (var item in IntList)
    {
        if (item > Max)
        {
            Max = item;
        }
    }
    return Max;
}
List<int> TestIntList2 = new List<int>() {-9,12,10,3,17,5};
// You should get back 17 in this example
int maxVAlue= FindMax(TestIntList2);
System.Console.WriteLine(maxVAlue);


// sqare 

static List<int> SquareValues(List<int> IntList)

{
    for (int i = 0; i <IntList.Count ; i++)
    {
        IntList[i]=IntList[i]*IntList[i];
    }
    
    return IntList;
}

List<int> TestIntList3 = new List<int>() {1,2,3,4,5};
// You should get back [1,4,9,16,25], think about how you will show that this worked

List<int> propertyIn=SquareValues(TestIntList3);
foreach (var item in propertyIn)
{
    System.Console.WriteLine(item);
}
System.Console.WriteLine(propertyIn);

// replace negative numbers with 0

static List<int> numberChange(List<int> TestList){
    for (int i = 0; i < TestList.Count; i++){
        if (TestList[i]<0)
        {
            TestList[i]=0;
        }
    }
    return TestList;

}
List<int> TestIntArray = new List<int>(){-1,2,3,-4,5};
List<int> changedList = numberChange(TestIntArray);

foreach (var item in changedList )
{
    System.Console.WriteLine(item);
}

//print dictionary

static void PrintDictionary(Dictionary<string,string> MyDictionary)
{
    foreach (var item in MyDictionary )
    {
        System.Console.WriteLine($"key is {item.Key} and value is {item.Value}");
        
    }
}
Dictionary<string,string> TestDict = new Dictionary<string,string>();
TestDict.Add("HeroName", "Iron Man");
TestDict.Add("RealName", "Tony Stark");
TestDict.Add("Powers", "Money and intelligence");
PrintDictionary(TestDict);

// find key

static void FindKey(Dictionary<string,string> MyDiction, string SearchTerm)
{
    foreach (var item in MyDiction )
    {
        if (item.Value == SearchTerm)
        {
            System.Console.WriteLine($"value: {SearchTerm} eshte tek key {item.Key}");
        }
    }
}
FindKey(TestDict, "Tony Stark" );

// generate dictionary 

static Dictionary<string, int> GenerateDictionary(List<string> wordsList, List<int> integersList ){
    Dictionary<string , int> NewDictionary= new Dictionary<string, int>();
    
    for (int i = 0; i < wordsList.Count; i++)
    {
        NewDictionary.Add(wordsList[i], integersList[i]);
    }
    return NewDictionary;

}

Dictionary<string ,int> newDiction= GenerateDictionary(TestStringList,TestIntArray);
foreach (var item in newDiction )
{
    System.Console.WriteLine($"{item.Key} me vlere {item.Value}");
}

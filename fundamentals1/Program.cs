
for (int i = 1; i <= 255; i++){
    System.Console.WriteLine(i);

}
// Random rand = new Random();
// for (int i=1; i <=5; i++){
//      Console.WriteLine(rand.Next(10,20));
// }

Random rand = new Random();
int sum=0;
for (int i=1; i <=5; i++){
    Console.WriteLine(rand.Next(10,20));
    sum = sum + rand.Next(10,20);
    System.Console.WriteLine(sum);

}

for(int i=1; i<=100; i++){
    if(i % 3 ==0 && i% 5 ==0){
        ;
    }
    else if(i % 3 ==0){
        System.Console.WriteLine("fizz");   //cw (i)
    }
        else if (i % 5 ==0) {

        System.Console.WriteLine("buzz");   //cw (i)
    }
}

for(int i=1; i<=100; i++){
    if(i % 3 ==0 && i% 5 ==0){
        System.Console.WriteLine("fizzBuzz");
    }
    else if(i % 3 ==0){
        System.Console.WriteLine("fizz");
    }
        else if (i % 5 ==0) {
            
        System.Console.WriteLine("buzz");
    }
}

# Testing 

## Unit 

f()

f(a(), b()) => mock/fake a() and b() 
Then you can test f(ma(), mb())



## Integration Test

function GetDataFromAPI_A() 
function GetDataFromAPI_B() 

function Processor(GetDataFromAPI_A() , GetDataFromAPI_B() ): boolean
{
    let resultA = GetDataFromAPI_A() 
    let resultB = GetDataFromAPI_B()
    
    return resultA === resultB
}




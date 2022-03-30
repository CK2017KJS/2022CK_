
public struct Data
{
    public int Num{get;set;}
    public readonly override string ToString()
    {
        return ("{0} is Data's Num ",Num);
    }
}


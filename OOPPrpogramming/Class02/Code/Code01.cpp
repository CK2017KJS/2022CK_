
#include <iostream>
#include <vecotr>
/*
한글 인코딩 깨짐 방지-




*/


using namespace std;

int iWidth,iHeight;

cout<<"Enter Buffer Width:";
cin>>iWidth;

cout<<"Enter Buffer Height:";
cin>>iHeight;

vector<char*> VectorChar;

for(int i=0; i<iHeight;i++)
{
    char* pCharArray = new char[iWidth];
    for(int j=0;j<iWidth;j++)
    {
        pCharArray[j] = (char)'A'+i;
    }
    VectorChar.push_back(pCharrArray);
}




for(int i=0; i<iHeight;i++)
{
    for(int j=0;j<iWidth;j++)
    {
        delete pCharArray[j];
    }

}



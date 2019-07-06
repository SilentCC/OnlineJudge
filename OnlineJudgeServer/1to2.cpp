#include<iostream>
#include<algorithm>
#include<random>
#include <fstream>
#include <sstream>

using namespace std;


int a[100005];
int b[100005][2];
int main()
{
    
    int n,m;
    scanf("%d%d",&n,&m);
   
    
    for(int i=0;i<n;i++)
     {
     scanf("%d",&a[i]);
     }
    int pos=0;
    for(int i=0;i<n;i++)
    {
        for(int j=i+1;j<n;j++)
        {
            if(a[i]+a[j]==m)
            {
                b[pos][0]=a[i];
                b[pos][1]=a[j];
                pos++;
            }
        }
    }
    
    /*sort(a,a+n);
    
    int i=0;int j=n-1;
    
    int pos=0;
    while(i<j)
    {
        if(a[i]+a[j]==m)
        {
            b[pos][0]=a[i];
            b[pos][1]=a[j];
            i++;
            j--;
            pos++;
        }
        else if(a[i]+a[j]>m)
        {
            j--;
        }
        else if(a[i]+a[j]<m)
        {
            i++;
        }
    }*/
    
  
    printf("%d\n",pos);
    
    for(int i=0;i<pos;i++)
    {
        printf("%d %d\n",b[i][0],b[i][1]);
        
    }
    
}


